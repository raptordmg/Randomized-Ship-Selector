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
            // Do constructor things
        }

        /// <summary>
        /// Get a list of ships from a local shipdata.json file.
        /// </summary>
        /// <param name="fileLocation">The location of the file. Base is executable folder.</param>
        /// <returns>A list of ships that is derived from the file.</returns>
        /// <exception cref="FileNotFoundException">Could not find local file.</exception>
        /// <exception cref="FileLoadException">The json does not have the expected format.</exception>
        public List<Ship> GetLocalShips(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                try
                {
                    JObject jObject = JObject.Parse(File.ReadAllText(fileLocation));
                    return JsonConvert.DeserializeObject<List<Ship>>(jObject["data"].ToString());
                }
                catch
                {
                    throw new FileLoadException("The shipdata file does not have a correct format. Try updating local data.");
                }
            }
            else
            {
                throw new FileNotFoundException("Local shipdata file not found. Try updating local data.");
            }
        }

        public string GetLocalVersion(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                using (Stream fileStream = File.OpenRead(fileLocation))
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    JObject jObject = JObject.Parse(reader.ReadToEnd());
                    return jObject["meta"]["wowsversion"].ToString();
                }
            }
            else
            {
                throw new FileNotFoundException("Local shipdata file not found");
            }
        }

        public JObject GetJObject(Uri webUri)
        {
            using (WebClient client = new WebClient())
            using (StreamReader reader = new StreamReader(client.OpenRead(webUri)))
            {
                return JObject.Parse(reader.ReadToEnd());

            }
        }

        public void DownloadFile(Uri webUri, string saveAs)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(webUri, saveAs);
            }
        }

        public void DownloadImages(Uri webUri, string saveAs)
        {
            return;
        }
    }
}
