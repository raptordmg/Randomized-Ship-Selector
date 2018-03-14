using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Net;

namespace Randomized_Ship_Selector
{
    public partial class Main : Form
    {
        private const string AppID = "68d50d230b5b9601ddd25f825c4a5b58";

        private List<Ship> AllShips = null;
        private List<Ship> PlayerShips = new List<Ship>();
        private Random Rnd = new Random();
        private bool UseIGN = false;

        private Ship CurrentShip = null;

        public Main()
        {
            InitializeComponent();

            AllShips = GetShipsFromResource();

            lbl_Count.Text = AllShips.Count.ToString();
        }

        /// Interactions ///

        // Selects a random ship
        private void btnRandom_Click(object sender, EventArgs e)
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

                if (count > 0) {
                    CurrentShip = filteredShips[Rnd.Next(filteredShips.Count)];
                    output.Image = CurrentShip.Image;
                }
            }
        }

        // Opens credits window
        private void btn_Credits_Click(object sender, EventArgs e)
        {
            Form creditForm = new Credits();
            creditForm.Show();
        }

        // Get User info
        private void btn_Search_Click(object sender, EventArgs e)
        {
            string server = cb_Server.Text;
            string userName = tb_UserName.Text;
            string userId = null;

            PlayerShips = new List<Ship>();

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
                    Log("Fetching account ID...");
                    string idUri = String.Format("https://api.worldofwarships.{0}/wows/account/list/?application_id={1}&search={2}", extension, AppID, userName);

                    JObject idObj = GetJsonFromWeb(idUri);

                    // T-T (cri every time)
                    JToken idData = idObj["data"].FirstOrDefault();

                    if (idData == null)
                    {
                        Log("WARNING: No player with specified name.");
                        return;
                    }

                    try
                    {
                        userId = idData["account_id"].ToString();
                        Log("Account ID fetched: " + userId);
                    }
                    catch
                    {
                        LogError("Could not fetch account ID");
                        return;
                    }

                    // Step 1.5:  Get if user account is private:
                    string privateUri = String.Format("https://api.worldofwarships.{0}/wows/account/info/?application_id={1}&account_id={2}", extension, AppID, userId);
                    JObject privateObject = GetJsonFromWeb(privateUri);
                    JToken privateData = privateObject["data"].First().First();
                    if (Boolean.Parse(privateData["hidden_profile"].ToString()))
                    {
                        Log("WARNING: User account is hidden, at this point in time RSS cannot prefilter while account is hidden.");
                        Log("No prefilter applied.");
                        return;
                    }

                    // Step 2: Get all ships that are in port
                    Log("Fetching all ships...");

                    string shipsUri = String.Format("https://api.worldofwarships.{0}/wows/ships/stats/?application_id={1}&account_id={2}&in_garage=1", extension, AppID, userId);
                    JObject shipsObj = GetJsonFromWeb(shipsUri);
                    try
                    {
                        JToken shipsData = shipsObj["data"][userId];

                        if(shipsData == null)
                        {
                            LogError("Could not find ships");
                            return;
                        }

                        foreach (JToken item in shipsData)
                        {
                            string id = item["ship_id"].ToString();

                            Ship aShip = AllShips.FirstOrDefault(s => s.ID == id);

                            if (aShip != null)
                                PlayerShips.Add(aShip);
                        }

                        Log("Finished fetching ships with more than 0 battles...");
                        Log("Total ships found: " + PlayerShips.Count);
                        UseIGN = true;
                        lbl_Count.Text = FilterShips(PlayerShips).Count.ToString();
                    }
                    catch
                    {
                        LogError("Problem summing up ships.");
                        return;
                    }
                }
            }
            else
            {
                Log("WARNING: Please select a valid server.");
                return;
            }
        }

        /// HELPERS ///

        // Imports all ships that are currently availaible.
        private List<Ship> GetShipsFromResource()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "Randomized_Ship_Selector.Resources.shipdata.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader r = new StreamReader(stream))
                {
                    string json = r.ReadToEnd();

                    List<Ship> ships = JsonConvert.DeserializeObject<List<Ship>>(json);

                    return ships;
                }
            }
        }

        /// <summary>
        /// Returns a JSON string
        /// </summary>
        /// <param name="url">Wargaming API URL</param>
        /// <returns>JSON String</returns>
        private JObject GetJsonFromWeb(string uri)
        {
            using (WebClient client = new WebClient())
            using (Stream stream = client.OpenRead(uri))
            using (StreamReader reader = new StreamReader(stream))
            {
                return JObject.Parse(reader.ReadLine());
            }
        }

        // Filters ships with set filters
        private List<Ship> FilterShips(List<Ship> ships)
        {
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

        // Fills tiers list for filtering
        private List<int> PopulateTiers()
        {
            List<int> tiers = new List<int>();

            if(cb_T1.Checked)
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
            if(cb_T4.Checked)
            {
                tiers.Add(4);
            }
            if(cb_T5.Checked) {
                tiers.Add(5);
            }
            if(cb_T6.Checked)
            {
                tiers.Add(6);
            }
            if(cb_T7.Checked)
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

        // Fills nations list for filtering
        private List<string> PopulateNations()
        {
            List<string> nations = new List<string>();

            if(cb_N_USN.Checked)
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

        // Fills classes list for filtering
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

        // Logs a string to the rich text box
        private void Log(string text)
        {
            rtb_SearchOutput.AppendText(Environment.NewLine + text);
        }

        private void LogError(string text)
        {
            rtb_SearchOutput.AppendText(Environment.NewLine + "ERROR: " + text);
            rtb_SearchOutput.AppendText(", this is an error. Please message me or open a issue on github:");
            rtb_SearchOutput.AppendText(Environment.NewLine + "https://github.com/DInbound/Randomized-Ship-Selector/issues");
        }

        // Scrolls down the rich text box
        private void rtb_SearchOutput_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            rtb_SearchOutput.SelectionStart = rtb_SearchOutput.Text.Length;
            // scroll it automatically
            rtb_SearchOutput.ScrollToCaret();
        }
    }
}
