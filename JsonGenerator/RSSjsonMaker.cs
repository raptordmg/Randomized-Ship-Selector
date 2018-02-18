using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace JsonGenerator
{
    internal class RSSjsonMaker
    {
        List<Ship> Ships = new List<Ship>();

        private const string AppID = "68d50d230b5b9601ddd25f825c4a5b58";

        // Ships to ignore by name
        private string[] IgnoreName = new string[] { "Cossack", "T-61", "Asashio", "Monaghan" };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path">Path to JSON file</param>
        public RSSjsonMaker(string path)
        {
            int pages = 1;

            for (int page = 1; page <= pages; page++)
            {
                Console.WriteLine("Reading page " + page);

                var json = GetJson(String.Format("https://api.worldofwarships.eu/wows/encyclopedia/ships/?application_id={0}&page_no={1}", AppID, page));
                pages = Int32.Parse(json["meta"]["page_total"].ToString());
                var data = json["data"];

                foreach (var item in data)
                {
                    Ship.Nations nation = GetNation(item.First()["nation"].ToString());
                    Ship.Classes shipClass = GetClasses(item.First()["type"].ToString());

                    string id = item.First()["ship_id"].ToString();
                    string name = item.First()["name"].ToString();

                    // Skip CB ships
                    if (name.Contains("["))
                        continue;

                    // Skip ignored by name
                    if (IgnoreName.Contains(name))
                        continue;

                    string resource = item.First()["ship_id_str"].ToString();
                    int tier = Int32.Parse(item.First()["tier"].ToString());
                    bool premium = GetPremium(item.First());

                    Ships.Add(new Ship(id , name, resource, tier, nation, shipClass, premium));
                }
            }
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

        public Ship.Nations GetNation(string name)
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
                return Ship.Nations.FN;
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

        public Ship.Classes GetClasses(string name)
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

        public bool GetPremium(JToken shipData)
        {
            if (Boolean.Parse(shipData["is_premium"].ToString()))
            {
                return true;
            }
            else if(Boolean.Parse(shipData["is_special"].ToString()))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a JSON string
        /// </summary>
        /// <param name="url">Wargaming API URL</param>
        /// <returns>JSON String</returns>
        private JObject GetJson(string uri)
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