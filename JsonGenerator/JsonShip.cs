using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

        public string imgName { get; }
        public int tier { get; }
        public string nation { get; }
        public string shipClass { get; set; }
        public bool premium { get; }

        /// <summary>
        /// A ship object
        /// </summary>
        /// <param name="img">Related Panzerschiffer image</param>
        /// <param name="tier">Tier of the ship (1 - 10)</param>
        /// <param name="cls">Class of the ship</param>
        /// <param name="premium">Is it a premium</param>
        public Ship(string imgName, int tier, Nations nation, Classes cls, bool premium)
        {
            this.imgName = imgName;

            if (tier > 0 && tier <= 10)
            {
                this.tier = tier;
            }
            else
            {
                throw new IndexOutOfRangeException("Tier must be between 1 and 10 inclusive.");
            }

            this.nation = nation.ToString();
            this.shipClass = cls.ToString();
            this.premium = premium;
        }
    }
}
