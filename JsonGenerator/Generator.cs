using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Net;

namespace JsonGenerator
{
    internal class Generator
    {
        public List<Ship> NewShips { get; }

        private List<Ship> OldShips = new List<Ship>();
        private readonly List<string> IgnoredShips = new List<string>();

        private const string outputDir = @"./output/";
        private const string fileName = @"./shipdata.json";
        private const string AppID = "68d50d230b5b9601ddd25f825c4a5b58";

        /// <summary>
        /// Constructor
        /// </summary>
        public Generator()
        {
            NewShips = new List<Ship>();

            int pages = 1;

            for (int page = 1; page <= pages; page++)
            {
                Console.WriteLine("Reading page " + page);

                JToken json = GetJsonFromWeb(String.Format("https://api.worldofwarships.eu/wows/encyclopedia/ships/?application_id={0}&page_no={1}", AppID, page));
                pages = Int32.Parse(json["meta"]["page_total"].ToString());

                JToken data = json["data"];

                NewShips.AddRange(CreateList(data));
            }
        }

        public List<Ship> CreateList(JToken data)
        {
            List<Ship> newShips = new List<Ship>();

            foreach (JToken item in data)
            {
                JToken shipData = item.First();

                string id = shipData["ship_id"].ToString();
                string name = shipData["name"].ToString();

                // Skip CB ships
                if (name.Contains("["))
                    continue;

                // Skip ignored by name
                if (Boolean.Parse(shipData["has_demo_profile"].ToString()))
                {
                    IgnoredShips.Add(name);
                    continue;
                }

                string resource = shipData["ship_id_str"].ToString();
                int tier = Int32.Parse(shipData["tier"].ToString());

                Ship.Nations nation = GetNation(shipData["nation"].ToString());
                Ship.Classes shipClass = GetClasses(shipData["type"].ToString());
                Ship.Status status = GetStatus(shipData);

                newShips.Add(new Ship(id, name, resource, tier, nation, shipClass, status));
            }

            return newShips;
        }

        public void MakeJson()
        {
            Console.WriteLine("Making JSON!");

            if(!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            using (StreamWriter file = File.CreateText(Path.Combine(outputDir, fileName)))
            {
                JsonSerializer s = new JsonSerializer();
                s.Serialize(file, NewShips);
            }

            Console.WriteLine("Success!");
        }

        public void PrintIgnoredShips()
        {
            Console.WriteLine();
            Console.WriteLine("Ships that have demo profile:");

            for (int i = 0; i < IgnoredShips.Count; i++)
            {
                Console.WriteLine(" - " + IgnoredShips[i]);
            }

            Console.WriteLine();
        }

        public List<Ship> GetDifference()
        {
            ReadExistingFile();

            List<Ship> difference = new List<Ship>();

            foreach (Ship newShip in NewShips)
            {
                bool found = false;

                foreach(Ship oldShip in OldShips)
                {
                    if(newShip.ID == oldShip.ID)
                    {
                        found = true;
                        break;
                    }
                }

                if(!found)
                    difference.Add(newShip);
            }

            return difference;
        }

        private void ReadExistingFile()
        {
            using (StreamReader file = File.OpenText(@"../../../Randomized Ship Selector/Resources/shipdata.json"))
            {
                string json = file.ReadToEnd();

                OldShips = JsonConvert.DeserializeObject<List<Ship>>(json);
            }
            
        }

        private Ship.Nations GetNation(string name)
        {
            if (name.Equals("germany"))
                return Ship.Nations.KM;
            if (name.Equals("japan"))
                return Ship.Nations.IJN;
            if (name.Equals("usa"))
                return Ship.Nations.USN;
            if (name.Equals("ussr"))
                return Ship.Nations.VMF;
            if (name.Equals("uk"))
                return Ship.Nations.RN;
            if (name.Equals("france"))
                return Ship.Nations.MN;
            if (name.Equals("pan_asia"))
                return Ship.Nations.PA;
            if (name.Equals("commonwealth"))
                return Ship.Nations.Commonwealth;
            if (name.Equals("italy"))
                return Ship.Nations.RM;
            if (name.Equals("poland"))
                return Ship.Nations.ORP;

            return Ship.Nations.None;
        }

        private Ship.Classes GetClasses(string name)
        {
            if (name.Equals("Cruiser"))
                return Ship.Classes.Cruiser;
            if (name.Equals("Battleship"))
                return Ship.Classes.Battleship;
            if (name.Equals("AirCarrier"))
                return Ship.Classes.Carrier;
            if (name.Equals("Destroyer"))
                return Ship.Classes.Destroyer;

            return Ship.Classes.None;
        }

        private Ship.Status GetStatus(JToken shipData)
        {
            if (Boolean.Parse(shipData["is_premium"].ToString()))
            {
                return Ship.Status.Premium;
            }
            else if(Boolean.Parse(shipData["is_special"].ToString()))
            {
                return Ship.Status.Special;
            }

            string name = shipData["name"].ToString();

            if (name.Contains("ARP"))
            {
                return Ship.Status.ARP;
            }

            return Ship.Status.Silver;
        }

        /// <summary>
        /// Returns a JSON string
        /// </summary>
        /// <param name="url">Wargaming API URL</param>
        /// <returns>JSON String</returns>
        private JObject GetJsonFromWeb(string uri)
        {
            using (WebClient client = new WebClient())
            using (Stream stream = client.OpenRead(uri))
            using (StreamReader reader = new StreamReader(stream))
            {
                return JObject.Parse(reader.ReadLine());
            }
        }
    }
}