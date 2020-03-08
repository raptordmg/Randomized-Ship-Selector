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

        private readonly List<string> _IgnoredShips = new List<string>();
        private readonly string _AppID = null;

        private const string OUTPUTDIR = @"./output/";
        private const string FILENAME = @"shipdata.json";

        /// <summary>
        /// Constructor
        /// </summary>
        public Generator(string appID)
        {
            NewShips = new List<Ship>();
            _AppID = appID;
        }

        public void GetNewShips()
        {
            NewShips.Clear();

            int pages = 1;

            for (int page = 1; page <= pages; page++)
            {
                JToken json = GetJsonFromWeb(String.Format("https://api.worldofwarships.eu/wows/encyclopedia/ships/?application_id={0}&page_no={1}", _AppID, page));
                pages = Int32.Parse(json["meta"]["page_total"].ToString());

                Console.WriteLine("Reading page " + page + "/" + pages);

                JToken data = json["data"];

                NewShips.AddRange(CreateList(data));
            }
        }

        public void MakeJson(string wowsVersion, string appVersion)
        {            
            Console.WriteLine("Making JSON!");

            if (!Directory.Exists(OUTPUTDIR))
                Directory.CreateDirectory(OUTPUTDIR);

            using (StreamWriter file = File.CreateText(Path.Combine(OUTPUTDIR, FILENAME)))
            {
                JsonSerializer s = new JsonSerializer();                
                s.Serialize(file, new JsonModel(wowsVersion, appVersion, NewShips));
            }
            Console.WriteLine("File created locally at {0}", Path.Combine(OUTPUTDIR, FILENAME).ToString());
            
        }

        public void UploadJson(Uri ftpUrl, NetworkCredential login)
        {
            Console.WriteLine("Uploading to FTP: " + ftpUrl);

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Credentials = login;
                    client.UploadFile(ftpUrl, Path.Combine(OUTPUTDIR, FILENAME));
                }
                Console.WriteLine("Upload Finished!");
            }
            catch (WebException ex)
            {
                Console.WriteLine("Uploading Failed: " + ex.Message);
            }
        }

        public void PrintIgnoredShips()
        {
            Console.WriteLine("Ships that have demo profile:");

            for (int i = 0; i < _IgnoredShips.Count; i++)
            {
                Console.WriteLine(" - " + _IgnoredShips[i]);
            }
        }

        public void PrintNewShips(Uri oldFile)
        {
            List<Ship> difference = GetDifference(oldFile);
            if (difference != null && difference.Count > 0)
            {
                Console.WriteLine("New Ships:");
                foreach (Ship s in difference)
                {
                    Console.WriteLine(s.ID + " - " + s.Name);
                }
            }
            else
            {
                Console.WriteLine("No new ships found.");
            }
        }

        public List<Ship> GetDifference(Uri oldFile)
        {
            JsonModel model = ReadExistingFile(oldFile);
            if (model == null || model.data.Count == 0)
            {
                // Return nothing because old file does not exist
                return null;
            }

            List<Ship> difference = new List<Ship>();

            foreach (Ship newShip in NewShips)
            {
                bool found = false;

                foreach (Ship oldShip in model.data)
                {
                    if (newShip.ID == oldShip.ID)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    difference.Add(newShip);
            }

            return difference;
        }

        private JsonModel ReadExistingFile(Uri fileLocation)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    string json = client.DownloadString(fileLocation);

                    Console.WriteLine("Found an existing file.");

                    return JsonConvert.DeserializeObject<JsonModel>(json);
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        if (ex.Response is FtpWebResponse response && response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                        {
                            Console.WriteLine("File '{0}' not found on server", fileLocation.ToString());
                            return null;
                        }
                        else
                        {
                            throw;
                        }
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        private List<Ship> CreateList(JToken data)
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
                    _IgnoredShips.Add(name);
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
                return Ship.Nations.PAS;
            if (name.Equals("pan_america"))
                return Ship.Nations.PAM;
            if (name.Equals("commonwealth"))
                return Ship.Nations.Commonwealth;
            if (name.Equals("italy"))
                return Ship.Nations.RM;
            if (name.Equals("europe"))
                return Ship.Nations.PAE;

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
            else if (Boolean.Parse(shipData["is_special"].ToString()))
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
        /// <param name="uri">Wargaming API URL</param>
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