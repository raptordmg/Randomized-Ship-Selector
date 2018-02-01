using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Randomized_Ship_Selector;
using System.Drawing;

namespace JsonGenerator
{
    internal class RSSjsonMaker
    {
        List<Ship> Ships = new List<Ship>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path">Path to JSON file</param>
        public RSSjsonMaker(string path)
        {
            List<string> fileNames = new List<string>();
            IEnumerable<string> files = Directory.EnumerateFiles(path);
            files.ToList().ForEach(e => fileNames.Add(e.Substring(path.Length).TrimEnd(".png".ToArray())));

            foreach (string item in fileNames)
            {
                // TODO: Fix tier
                // TODO: Fix premium

                string name = item + ".png";

                int tier = 1;

                Ship.Nations nation = Ship.Nations.None;

                string i = item.Substring(1, 1);

                switch (item.Substring(1, 1))
                {
                    default:
                        nation = Ship.Nations.None;
                        break;
                    case "J":
                        nation = Ship.Nations.IJN;
                        break;
                    case "A":
                        nation = Ship.Nations.USN;
                        break;
                    case "B":
                        nation = Ship.Nations.RN;
                        break;
                    case "F":
                        nation = Ship.Nations.FN;
                        break;
                    case "G":
                        nation = Ship.Nations.KM;
                        break;
                    case "I":
                        nation = Ship.Nations.RM;
                        break;
                    case "R":
                        nation = Ship.Nations.VMF;
                        break;
                    case "U":
                        nation = Ship.Nations.Commonwealth;
                        break;
                    case "W":
                        nation = Ship.Nations.ORP;
                        break;
                    case "Z":
                        nation = Ship.Nations.PA;
                        break;
                }

                Ship.Classes cls = Ship.Classes.None;

                switch (item.Substring(3, 1))
                {
                    default:
                        cls = Ship.Classes.None;
                        break;
                    case "A":
                        cls = Ship.Classes.Carrier;
                        break;
                    case "B":
                        cls = Ship.Classes.Battleship;
                        break;
                    case "C":
                        cls = Ship.Classes.Cruiser;
                        break;
                    case "D":
                        cls = Ship.Classes.Destroyer;
                        break;
                }

                bool prem = false; // IsPremium(item);

                Ships.Add(new Ship(name, tier, nation, cls, prem));
            }
        }

        public bool IsPremium(string imageName)
        {
            string name = imageName + ".png";

            using (FileStream fs = new FileStream($"C:/Users/Daan/Source/Repos/Randomized-Ship-Selector/Randomized Ship Selector/Resources/Panzerschiffer_Icons/" + name, FileMode.Open))
            {
                Image img = Image.FromStream(fs);
                Bitmap bmp = (Bitmap)img;

                List<Color> pixels = new List<Color>();

                pixels.Add(bmp.GetPixel(110, 5));
                pixels.Add(bmp.GetPixel(111, 5));
                pixels.Add(bmp.GetPixel(112, 5));
                pixels.Add(bmp.GetPixel(113, 5));
                pixels.Add(bmp.GetPixel(114, 5));
                pixels.Add(bmp.GetPixel(115, 5));
                pixels.Add(bmp.GetPixel(116, 5));
                pixels.Add(bmp.GetPixel(117, 5));
                pixels.Add(bmp.GetPixel(118, 5));
                pixels.Add(bmp.GetPixel(119, 5));
                pixels.Add(bmp.GetPixel(120, 5));

                // Alpha == 255;
                for (int i = 0; i < pixels.Count; i++)
                {
                    if(pixels[i].A == 255)
                    {
                        // TODO fix hue
                        float hue = pixels[i].GetHue();
                        float sat = pixels[i].GetSaturation();
                        Console.WriteLine(hue);
                        Console.WriteLine(sat);
                        if(hue > 10 && hue < 100)
                        {
                            return true;
                        } else
                        {
                            return false;
                        }
                    }
                }
            }

            // Something went wrong, assume is false;
            Console.WriteLine("Error in parsing if ship is premium.");
            return false;
        }

        public void MakeJson()
        {
            Console.WriteLine("Making JSON!");

            // ../../../Randomized Ship Selector/Resources
            using (StreamWriter file = File.CreateText(@"../../../Randomized Ship Selector/Resources/shipdata.json"))
            {
                JsonSerializer s = new JsonSerializer();
                s.Serialize(file, Ships);
            }

            Console.WriteLine("Success!");
        }
    }
}