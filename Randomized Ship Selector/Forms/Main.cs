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
         

            List<int> tiers = PopulateTiers();
            List<string> nations = PopulateNations();
            List<string> classes = PopulateClasses();

            List<Ship> filtered;

            // Filter ships
            filtered = ships.Where(s => (s.ShipStatus == Ship.Status.Premium && prem)
                                        || (s.ShipStatus == Ship.Status.Special && prem)
                                        || (s.ShipStatus == Ship.Status.Silver && nonprem))
                .Where(s => tiers.Contains(s.Tier))
                .Where(s => nations.Contains(s.Nation.ToString()))
                .Where(s => classes.Contains(s.ShipClass.ToString()))
                    .ToList();

            return filtered;
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
            if (cb_T11.Checked)
            {
                tiers.Add(11);
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
            if (cb_N_PAS.Checked)
            {
                nations.Add("PAS");
            }
            if(cb_N_PAM.Checked)
            {
                nations.Add("PAM");
            }
            if (cb_N_RN.Checked)
            {
                nations.Add("RN");
            }
            if (cb_N_PAE.Checked)
            {
                nations.Add("PAE");
            }
            if (cb_N_Commonwealth.Checked)
            {
                nations.Add("Commonwealth");
            }
            if (cb_N_Netherlands.Checked)
            {
                nations.Add("Netherlands");
            }
            if (cb_N_Spain.Checked)
            {
                nations.Add("Spain");
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
            if (cb_C_Submarine.Checked)
            {
                classes.Add("Submarine");
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
            cb_T11.Checked = c;
        }

        private void Cb_Nations_CheckedChanged(object sender, EventArgs e)
        {
            bool c = cb_Nations.Checked;
            cb_N_Commonwealth.Checked = c;
            cb_N_FN.Checked = c;
            cb_N_IJN.Checked = c;
            cb_N_KM.Checked = c;
            cb_N_PAE.Checked = c;
            cb_N_PAS.Checked = c;
            cb_N_PAM.Checked = c;
            cb_N_RM.Checked = c;
            cb_N_RN.Checked = c;
            cb_N_USN.Checked = c;
            cb_N_VMF.Checked = c;
            cb_N_Spain.Checked = c;
            cb_N_Netherlands.Checked = c;
        }

        private void Cb_Classes_CheckedChanged(object sender, EventArgs e)
        {
            bool c = cb_Classes.Checked;
            cb_C_Battleship.Checked = c;
            cb_C_Carrier.Checked = c;
            cb_C_Cruiser.Checked = c;
            cb_C_Destroyer.Checked = c;
            cb_C_Submarine.Checked = c;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
