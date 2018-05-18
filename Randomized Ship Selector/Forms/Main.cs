using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Net;

namespace Randomized_Ship_Selector
{
    public partial class Main : Form
    {
        // Lists of ships
        private List<Ship> AllShips = null;
        private List<Ship> PlayerShips = new List<Ship>();

        // Helper Variables
        private Random Rnd = new Random();
        private bool UseIGN = false;

        private Ship CurrentShip = null;

        // Helper Objects
        private ConnectionController CC;
        private Config Config;
        private Logger Logger;

        public Main()
        {
            InitializeComponent();

            CC = new ConnectionController();
            Config = new Config();
            Logger = new Logger(rtb_SearchOutput);

            // Start with an update to the master list
            UpdateMasterList();
        }

        /// Interactions ///

        // Selects a random ship
        private void BtnRandom_Click(object sender, EventArgs e)
        {
            if (UseIGN)
            {
                PictureBox output = pbOutput;
                List<Ship> filteredShips = FilterShips(PlayerShips);

                int count = filteredShips.Count;

                lbl_Count.Text = count.ToString();

                if (count > 0)
                {
                    CurrentShip = filteredShips[Rnd.Next(filteredShips.Count)];

                    output.Image = CurrentShip.Image;
                }
            }
            else
            {
                PictureBox output = pbOutput;
                List<Ship> filteredShips = FilterShips(AllShips);

                int count = filteredShips.Count;

                lbl_Count.Text = count.ToString();

                if (count > 0)
                {
                    CurrentShip = filteredShips[Rnd.Next(filteredShips.Count)];
                    output.Image = CurrentShip.Image;
                }
            }
        }

        // Get User info
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            SearchPlayer();
        }

        private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateLocalData();
        }

        private void CreditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form creditForm = new Credits();
            creditForm.Show();
        }

        /// HELPERS ///

        private void UpdateMasterList()
        {
            try
            {
                AllShips = CC.GetLocalShips(Config.LocalShipDataJson);
            }
            catch (Exception ex) when (ex is FileLoadException || ex is FileNotFoundException)
            {
                Logger.Log(ex.Message);
                return;
            }

            int count = AllShips.Count;
            Logger.Log("Loaded " + count.ToString() + " ships.");
            lbl_Count.Text = count.ToString();
        }

        // Filters ships with set filters
        private List<Ship> FilterShips(List<Ship> ships)
        {
            if(ships == null || ships.Count <= 0)
            {
                Logger.Log("No ships to filter on.");
            }

            bool nonprem = cb_nonPremium.Checked;
            bool prem = cb_Premium.Checked;
            bool arp = cb_ARP.Checked;

            List<int> tiers = PopulateTiers();
            List<string> nations = PopulateNations();
            List<string> classes = PopulateClasses();

            List<Ship> filtered;

            // Filter ships
            filtered = ships.Where(s => (s.ShipStatus == Ship.Status.Premium && prem)
                                        || (s.ShipStatus == Ship.Status.Special && prem)
                                        || (s.ShipStatus == Ship.Status.Silver && nonprem)
                                        || (s.ShipStatus == Ship.Status.ARP && arp))
                .Where(s => tiers.Contains(s.Tier))
                .Where(s => nations.Contains(s.Nation.ToString()))
                .Where(s => classes.Contains(s.ShipClass.ToString()))
                    .ToList();

            return filtered;
        }

        // Gets player and fills playerships list and set UseIGN to true.
        private void SearchPlayer()
        {
            string server = cb_Server.Text;
            string userName = tb_UserName.Text;
            string userId = null;

            PlayerShips = new List<Ship>();

            if(AllShips == null || AllShips.Count <= 0)
            {
                Logger.Log("No local ships found. Try updating first.");
            }

            if (server.Equals("ru") || server.Equals("eu") || server.Equals("na") || server.Equals("asia"))
            {
                string extension = "";

                if (server.Equals("na"))
                {
                    extension = "com";
                }
                else
                {
                    extension = server;
                }

                if (userName != "")
                {
                    // Step 1: Get user ID
                    Logger.Log("Fetching account ID...");
                    Uri idUri = new Uri(String.Format("https://api.worldofwarships.{0}/wows/account/list/?application_id={1}&search={2}", extension, Config.AppID, userName));

                    JObject idObj;
                    try
                    {
                        idObj = CC.GetJObject(idUri);
                    }
                    catch (WebException ex)
                    {
                        Logger.CatchWebEx(ex);
                        return;
                    }

                    // T-T (cri every time)
                    JToken idData = idObj["data"].FirstOrDefault();

                    if (idData == null)
                    {
                        Logger.Log("WARNING: No player with specified name.");
                        return;
                    }

                    try
                    {
                        userId = idData["account_id"].ToString();
                        Logger.Log("Account ID fetched: " + userId);
                    }
                    catch
                    {
                        Logger.LogError("Could not fetch account ID.");
                        return;
                    }

                    // Step 1.5:  Get if user account is private:
                    Uri privateUri = new Uri(String.Format("https://api.worldofwarships.{0}/wows/account/info/?application_id={1}&account_id={2}", extension, Config.AppID, userId));

                    JObject privateObject;
                    try
                    {
                        privateObject = CC.GetJObject(privateUri);
                    }
                    catch (WebException ex)
                    {
                        Logger.CatchWebEx(ex);
                        return;
                    }

                    JToken privateData = privateObject["data"].First().First();
                    if (Boolean.Parse(privateData["hidden_profile"].ToString()))
                    {
                        Logger.Log("WARNING: User account is hidden, at this point in time RSS cannot prefilter while account is hidden.");
                        Logger.Log("No prefilter applied.");
                        return;
                    }

                    // Step 2: Get all ships that are in port
                    Logger.Log("Fetching all ships...");

                    Uri shipsUri = new Uri(String.Format("https://api.worldofwarships.{0}/wows/ships/stats/?application_id={1}&account_id={2}&in_garage=1", extension, Config.AppID, userId));
                    JObject shipsObj;
                    try
                    {
                        shipsObj = CC.GetJObject(shipsUri);
                    }
                    catch (WebException ex)
                    {
                        Logger.CatchWebEx(ex);
                        return;
                    }

                    try
                    {
                        JToken shipsData = shipsObj["data"][userId];

                        if (shipsData == null)
                        {
                            Logger.LogError("Could not find ships");
                            return;
                        }

                        foreach (JToken item in shipsData)
                        {
                            string id = item["ship_id"].ToString();

                            Ship aShip = AllShips.FirstOrDefault(s => s.ID == id);

                            if (aShip != null)
                                PlayerShips.Add(aShip);
                        }

                        Logger.Log("Finished fetching ships with more than 0 battles...");
                        Logger.Log("Total ships found: " + PlayerShips.Count);
                        UseIGN = true;
                        lbl_Count.Text = FilterShips(PlayerShips).Count.ToString();
                    }
                    catch
                    {
                        Logger.LogError("Problem summing up ships.");
                    }
                }
            }
            else
            {
                Logger.Log("WARNING: Please select a valid server.");
            }
        }

        private void UpdateLocalData()
        {
            Logger.Log("Updating...");

            try
            {
                // Download new JSON
                CC.DownloadFile(Config.WebShipDataJson, Config.LocalShipDataJson);
                
                // TODO: Download new images
            }
            catch (WebException ex)
            {
                Logger.CatchWebEx(ex);
                return;
            }
            
            Logger.Log("File(s) successfully updated!");
            UpdateMasterList();
        }

        // Scrolls down the rich text box
        private void Rtb_SearchOutput_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            rtb_SearchOutput.SelectionStart = rtb_SearchOutput.Text.Length;
            // scroll it automatically
            rtb_SearchOutput.ScrollToCaret();
        }

        /// Populaters ///
        
        // Fills tiers list for filtering based on checked checkboxes.
        private List<int> PopulateTiers()
        {
            List<int> tiers = new List<int>();

            if (cb_T1.Checked)
            {
                tiers.Add(1);
            }
            if (cb_T2.Checked)
            {
                tiers.Add(2);
            }
            if (cb_T3.Checked)
            {
                tiers.Add(3);
            }
            if (cb_T4.Checked)
            {
                tiers.Add(4);
            }
            if (cb_T5.Checked)
            {
                tiers.Add(5);
            }
            if (cb_T6.Checked)
            {
                tiers.Add(6);
            }
            if (cb_T7.Checked)
            {
                tiers.Add(7);
            }
            if (cb_T8.Checked)
            {
                tiers.Add(8);
            }
            if (cb_T9.Checked)
            {
                tiers.Add(9);
            }
            if (cb_T10.Checked)
            {
                tiers.Add(10);
            }

            return tiers;
        }

        // Fills nations list for filtering based on checked checkboxes.
        private List<string> PopulateNations()
        {
            List<string> nations = new List<string>();

            if (cb_N_USN.Checked)
            {
                nations.Add("USN");
            }
            if (cb_N_IJN.Checked)
            {
                nations.Add("IJN");
            }
            if (cb_N_VMF.Checked)
            {
                nations.Add("VMF");
            }
            if (cb_N_FN.Checked)
            {
                nations.Add("MN");
            }
            if (cb_N_RM.Checked)
            {
                nations.Add("RM");
            }
            if (cb_N_KM.Checked)
            {
                nations.Add("KM");
            }
            if (cb_N_PA.Checked)
            {
                nations.Add("PA");
            }
            if (cb_N_RN.Checked)
            {
                nations.Add("RN");
            }
            if (cb_N_ORP.Checked)
            {
                nations.Add("ORP");
            }
            if (cb_N_Commonwealth.Checked)
            {
                nations.Add("Commonwealth");
            }

            return nations;
        }

        // Fills classes list for filtering based on checked checkboxes.
        private List<string> PopulateClasses()
        {
            List<string> classes = new List<string>();

            if (cb_C_Battleship.Checked)
            {
                classes.Add("Battleship");
            }
            if (cb_C_Carrier.Checked)
            {
                classes.Add("Carrier");
            }
            if (cb_C_Cruiser.Checked)
            {
                classes.Add("Cruiser");
            }
            if (cb_C_Destroyer.Checked)
            {
                classes.Add("Destroyer");
            }

            return classes;
        }

        private void Cb_Tiers_CheckedChanged(object sender, EventArgs e)
        {
            bool c = cb_Tiers.Checked;
            cb_T1.Checked = c;
            cb_T2.Checked = c;
            cb_T3.Checked = c;
            cb_T4.Checked = c;
            cb_T5.Checked = c;
            cb_T6.Checked = c;
            cb_T7.Checked = c;
            cb_T8.Checked = c;
            cb_T9.Checked = c;
            cb_T10.Checked = c;
        }

        private void Cb_Nations_CheckedChanged(object sender, EventArgs e)
        {
            bool c = cb_Nations.Checked;
            cb_N_Commonwealth.Checked = c;
            cb_N_FN.Checked = c;
            cb_N_IJN.Checked = c;
            cb_N_KM.Checked = c;
            cb_N_ORP.Checked = c;
            cb_N_PA.Checked = c;
            cb_N_RM.Checked = c;
            cb_N_RN.Checked = c;
            cb_N_USN.Checked = c;
            cb_N_VMF.Checked = c;
        }

        private void Cb_Classes_CheckedChanged(object sender, EventArgs e)
        {
            bool c = cb_Classes.Checked;
            cb_C_Battleship.Checked = c;
            cb_C_Carrier.Checked = c;
            cb_C_Cruiser.Checked = c;
            cb_C_Destroyer.Checked = c;
        }

        private void checkVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckVersions();
        }

        private void CheckVersions()
        {
            Logger.Log("Checking Versions");
            JToken remote = CC.GetRemoteVersionNumbers(Config.WebVersionAPI);
            JToken local = CC.GetLocalVersionNumbers(Config.LocalShipDataJson);

            Logger.Log("Shipdata version: Current " + local["wowsversion"] + " / Newest " + remote["wowsversion"]);

            // TODO: Check versions on startup.
            // TODO: Message if there is a newer app version.
        }
    }
}
