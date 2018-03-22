using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Randomized_Ship_Selector
{
    public class ConnectionController
    {
        public ConnectionController()
        {

        }

        public JObject GetJObject(Uri webUri)
        {
            using (WebClient client = new WebClient())
            using (StreamReader reader = new StreamReader(client.OpenRead(webUri)))
            {
                return JObject.Parse(reader.ReadToEnd());

            }
        }

        public List<Ship> GetShips(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                using (Stream fileStream = File.OpenRead(fileLocation))
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    return JsonConvert.DeserializeObject<List<Ship>>(reader.ReadToEnd());
                }
            }
            else
            {
                throw new FileNotFoundException("Local shipdata file not found");
            }
        }

        public bool DownloadFile(Uri webUri, string saveAs)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(webUri, saveAs);
                return true;
            }
        }

        public bool DownloadFile(Uri webUri, string saveAs, Uri localPath)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(webUri, Path.Combine(localPath.ToString(), saveAs));
                return true;
            }
        }
    }
}
