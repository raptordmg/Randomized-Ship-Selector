using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
            public string ShipDataFileName { get; }
            public NetworkCredential FtpLogin { get; }

            public Settings(XmlDocument doc)
            {
                AppID = doc.DocumentElement.SelectSingleNode("wgAppID").InnerText;
                FtpUrl = new Uri(doc.DocumentElement.SelectSingleNode("ftpData/ftpUrl").InnerText);
                ShipDataUrl = new Uri(doc.DocumentElement.SelectSingleNode("ftpData/shipDataUrl").InnerText);
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
            gen.PrintNewShips(settings.ShipDataUrl);

            Console.WriteLine();
            gen.PrintIgnoredShips();
            
            Console.Write(Environment.NewLine + "Generate new Json? y/n ");
            ConsoleKey response2 = Console.ReadKey().Key;
            Console.WriteLine();

            if (response2 == ConsoleKey.Y)
            {
                Console.WriteLine("Current WoWs Version:");
                string wowsVersion = Console.ReadLine();

                Console.WriteLine("Current App Version:");
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
    }
}
