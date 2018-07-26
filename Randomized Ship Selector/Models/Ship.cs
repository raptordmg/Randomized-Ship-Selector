using Ionic.Zip;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Randomized_Ship_Selector
{
    public class Ship
    {
        private const string ZIPLOCATION = @"Resources/PanzerschifferIcons.zip";

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
            PAS, // Pan-Asian Navy (Pan-Asia)
            PAM, // Pan-Ameracan Navy (Pan-America)
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
            this.Image = GetImage(imageName + ".png");
            this.Tier = tier;
            this.Nation = nation;
            this.ShipClass = shipClass;
            this.ShipStatus = shipStatus;
        }
        
        private Image GetImage(string imageName)
        {
            if (File.Exists(ZIPLOCATION))            {

                using (ZipFile zip = ZipFile.Read(ZIPLOCATION))
                {
                    ZipEntry e = zip["Panzerschiffer_Icons/" + imageName];

                    using (MemoryStream ms = new MemoryStream())
                    {
                        e.Extract(ms);
                        return Image.FromStream(ms);
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("Cannot find the file: " + ZIPLOCATION + " try reinstalling or updating local data.");
            }
        }
    }
}
