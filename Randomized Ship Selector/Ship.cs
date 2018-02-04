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
            USN, // United States Navy
            IJN, // Imperial Japanese Navy (Japan)
            VMF, // Vojenno-Morskoj Flot (Russia)
            FN, // La Royale (French)
            RM, // Regina Marina (Italy)
            KM, // Kriegsmarine (Germany)
            PA, // Pan-Asian Navy (Pan-Asia)
            RN, // Royal Navy (England)
            ORP, // Okręt Rzeczypospolitej Polskiej (Poland)
            Commonwealth // Perth, Vampire (Australia)
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
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "Randomized_Ship_Selector.Resources.Panzerschiffer_Icons." + imgName;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                this.Image = Image.FromStream(stream);
            }

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
