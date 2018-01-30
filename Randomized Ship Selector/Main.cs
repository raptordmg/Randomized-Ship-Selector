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

namespace Randomized_Ship_Selector
{
    public partial class Main : Form
    {
        private List<Ship> Ships = null;
        private Random Rnd = new Random();

        public Main()
        {
            InitializeComponent();

            Ships = ImportShipsFromFile("./Resources/ships.json");
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            PictureBox output = pbOutput;

            Ship randomShip = Ships[Rnd.Next(Ships.Count)];
            output.Image = randomShip.Image;
        }

        private List<Ship> ImportShipsFromFile(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();

                List<Ship> ships = JsonConvert.DeserializeObject<List<Ship>>(json);

                return ships;
            }
        }
    }
}
