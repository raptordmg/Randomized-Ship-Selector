using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Randomized_Ship_Selector
{
    public class Ship
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
            USN, // United States Navy
            IJN, // Imperial Japanese Navy (Japan)
            VMF, // Vojenno-Morskoj Flot (Russia)
            MN, // Marine Nationale (French)
            RM, // Regina Marina (Italy)
            KM, // Kriegsmarine (Germany)
            PA, // Pan-Asian Navy (Pan-Asia)
            RN, // Royal Navy (England)
            ORP, // Okręt Rzeczypospolitej Polskiej (Poland)
            Commonwealth // Perth, Vampire (Australia)
        }

        public enum Status
        {
            None,
            Silver,
            Premium,
            Special,
            ARP
        }

        public string ID { get; }
        public string Name { get; }
        public Image Image { get; }
        public int Tier { get; }
        public Nations Nation { get; }
        public Classes ShipClass { get; }
        public Status ShipStatus { get; }

        /// <summary>
        /// A ship object
        /// </summary>
        /// <param name="img">Related Panzerschiffer image</param>
        /// <param name="tier">Tier of the ship (1 - 10)</param>
        /// <param name="shipClass">Class of the ship</param>
        /// <param name="shipStatus">Premium status</param>
        public Ship(string id, string name, string imageName, int tier, Nations nation, Classes shipClass, Status shipStatus)
        {
            this.ID = id;
            this.Name = name;

            string resourceName = @"Resources/Panzerschiffer_Icons/" + imageName + ".png";

            using (Stream stream = new FileStream(resourceName, FileMode.Open))
            {
                this.Image = Image.FromStream(stream);
            }

            this.Tier = tier;
            this.Nation = nation;
            this.ShipClass = shipClass;
            this.ShipStatus = shipStatus;
        }
    }
}
