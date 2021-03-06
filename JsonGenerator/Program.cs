﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace JsonGenerator
{
    static class Program
    {
        class Settings
        {
            public string AppID { get; }
            public Uri FtpUrl { get; }
            public Uri ShipDataUrl { get; }
            public Uri ApiUrl { get; }
            public string ShipDataFileName { get; }
            public NetworkCredential FtpLogin { get; }

            public Settings(XmlDocument doc)
            {
                AppID = doc.DocumentElement.SelectSingleNode("wgAppID").InnerText;
                FtpUrl = new Uri(doc.DocumentElement.SelectSingleNode("ftpData/ftpUrl").InnerText);
                ShipDataUrl = new Uri(doc.DocumentElement.SelectSingleNode("ftpData/shipDataUrl").InnerText);
                ApiUrl = new Uri(doc.DocumentElement.SelectSingleNode("ftpData/apiUrl").InnerText);
                ShipDataFileName = doc.DocumentElement.SelectSingleNode("ftpData/jsonFile").InnerText;
                FtpLogin = new NetworkCredential(doc.DocumentElement.SelectSingleNode("ftpData/userName").InnerText, doc.DocumentElement.SelectSingleNode("ftpData/userPassword").InnerText);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Ship Json Generator Tool");

            // Load Settings
            XmlDocument settingsDoc = new XmlDocument();
            settingsDoc.Load("./generator.config");
            Settings settings = new Settings(settingsDoc);
                
            // Set up variables
            Uri shipDataJsonUri = new Uri(settings.FtpUrl, settings.ShipDataFileName);
            Generator gen = new Generator(settings.AppID);

            // Get new ships
            gen.GetNewShips();

            // Print for interface
            Console.WriteLine();
            Console.Write("Search for old file? y/n ");
            ConsoleKey response4 = Console.ReadKey().Key;
            Console.WriteLine();

            if(response4 == ConsoleKey.Y)
            {
                gen.PrintNewShips(settings.ShipDataUrl);
            }

            Console.WriteLine();
            gen.PrintIgnoredShips();
            
            Console.Write(Environment.NewLine + "Generate new Json? y/n ");
            ConsoleKey response2 = Console.ReadKey().Key;
            Console.WriteLine();

            if (response2 == ConsoleKey.Y)
            {
                JToken meta = GetRemoteVersionNumbers(settings.ApiUrl);

                Console.WriteLine("WoWs Version (Current=" + meta["wowsversion"] + ")");
                string wowsVersion = Console.ReadLine();

                Console.WriteLine("App Version (Current=" + meta["appversion"] + ")");
                string appVersion = Console.ReadLine();

                gen.MakeJson(wowsVersion, appVersion);

                Console.WriteLine();
                Console.Write("Upload to server? y/n ");
                ConsoleKey response3 = Console.ReadKey().Key;
                Console.WriteLine();

                if (response3 == ConsoleKey.Y)
                {
                    gen.UploadJson(shipDataJsonUri, settings.FtpLogin);
                }
            }

            Console.WriteLine("Press <Enter> to exit.");
            Console.ReadLine();
        }

        public static JToken GetRemoteVersionNumbers(Uri apiLocation)
        {
            using (WebClient client = new WebClient())
            using (StreamReader reader = new StreamReader(client.OpenRead(apiLocation)))
            {
                return JObject.Parse(reader.ReadToEnd());
            }
        }
    }
}
