using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randomized_Ship_Selector
{
    class Ship
    {
        public enum Classes
        {
            None,
            Destroyer,
            Cruiser,
            Battleship,
            Carrier
        }

        public enum Nations
        {
            None,
            USN,
            IJN,
            VMF,
            KM,
            PA,
            RN
        }

        public Image Image { get; }
        public int Tier { get; }
        public Nations Nation { get; }
        public Classes ShipClass { get; set; }
        public bool Premium { get; }

        /// <summary>
        /// A ship object
        /// </summary>
        /// <param name="img">Related Panzerschiffer image</param>
        /// <param name="tier">Tier of the ship (1 - 10)</param>
        /// <param name="cls">Class of the ship</param>
        /// <param name="premium">Is it a premium</param>
        public Ship(string imgName, int tier, Nations nation, Classes cls, bool premium)
        {
            FileStream fs = new FileStream("Resources/Panzerschiffer_Icons/" + imgName, FileMode.Open);
            this.Image = Image.FromStream(fs);
            fs.Close();

            if (tier > 0 && tier <= 10)
            {
                this.Tier = tier;
            }
            else
            {
                throw new IndexOutOfRangeException("Tier must be between 1 and 10 inclusive.");
            }

            this.Nation = nation;
            this.ShipClass = cls;
            this.Premium = premium;
        }
    }
}
