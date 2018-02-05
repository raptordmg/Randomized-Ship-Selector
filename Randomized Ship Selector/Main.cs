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
using System.Reflection;

namespace Randomized_Ship_Selector
{
    public partial class Main : Form
    {
        private List<Ship> AllShips = null;
        private Random Rnd = new Random();

        public Main()
        {
            InitializeComponent();

            AllShips = ImportShipsFromFile();            

            lbl_Count.Text = AllShips.Count().ToString();
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            PictureBox output = pbOutput;
            List<Ship> filteredShips = FilterShips(AllShips);

            int count = filteredShips.Count();

            lbl_Count.Text = count.ToString();

            if (count > 0) {
                Ship randomShip = filteredShips[Rnd.Next(filteredShips.Count)];
                output.Image = randomShip.Image;
            }
        }

        private List<Ship> ImportShipsFromFile()
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

        private List<Ship> FilterShips(List<Ship> ships)
        {
            bool nonprem = cb_nonPremium.Checked;
            bool prem = cb_Premium.Checked;
            List<int> tiers = PopulateTiers();
            List<string> nations = PopulateNations();
            List<string> classes = PopulateClasses();

            List<Ship> filtered = new List<Ship>();

            // Filter ships
            filtered = ships.Where(s => (s.Premium == true && prem) || (s.Premium == false && nonprem))
                .Where(s => tiers.Contains(s.Tier))
                .Where(s => nations.Contains(s.Nation.ToString()))
                .Where(s => classes.Contains(s.ShipClass.ToString()))
                    .ToList<Ship>();

            return filtered;
        }

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
                nations.Add("FN");
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

        private void btn_Credits_Click(object sender, EventArgs e)
        {
            Form creditForm = new Credits();
            creditForm.Show();
        }

        private void cb_N_Commonwealth_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
