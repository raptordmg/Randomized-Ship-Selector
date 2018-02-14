using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace JsonGenerator
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

        public string ID { get; }
        public string Name { get; }
        public string ImageName { get; }
        public int Tier { get; }
        public string Nation { get; }
        public string ShipClass { get; set; }
        public bool Premium { get; }

        /// <summary>
        /// A ship object
        /// </summary>
        /// <param name="img">Related Panzerschiffer image</param>
        /// <param name="tier">Tier of the ship (1 - 10)</param>
        /// <param name="cls">Class of the ship</param>
        /// <param name="premium">Is it a premium</param>
        public Ship(string id, string name, string imgName, int tier, Nations nation, Classes cls, bool premium)
        {
            this.ID = id;
            this.Name = name;
            this.ImageName = imgName;

            if (tier > 0 && tier <= 10)
            {
                this.Tier = tier;
            }
            else
            {
                throw new IndexOutOfRangeException("Tier must be between 1 and 10 inclusive.");
            }

            this.Nation = nation.ToString();
            this.ShipClass = cls.ToString();
            this.Premium = premium;
        }
    }
}
