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
            public string ShipDataFileName { get; }
            public NetworkCredential FtpLogin { get; }

            public Settings(string appId, string ftpUrl, string sdFileName, string userName, string userPassword)
            {
                AppID = appId;
                FtpUrl = new Uri(ftpUrl);
                ShipDataFileName = sdFileName;
                FtpLogin = new NetworkCredential(userName, userPassword);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Ship Json Generator Tool");

            XmlDocument config = new XmlDocument();
            config.Load("./generator.config");

            Settings settings = new Settings(
                config.DocumentElement.SelectSingleNode("wgAppID").InnerText,
                config.DocumentElement.SelectSingleNode("ftpData/ftpUrl").InnerText,
                config.DocumentElement.SelectSingleNode("ftpData/jsonFile").InnerText,
                config.DocumentElement.SelectSingleNode("ftpData/userName").InnerText,
                config.DocumentElement.SelectSingleNode("ftpData/userPassword").InnerText);
                
            Uri shipDataJsonUri = new Uri(settings.FtpUrl, settings.ShipDataFileName);
            Generator gen = new Generator(settings.AppID);


            gen.GetNewShips();
            gen.PrintIgnoredShips();

            Console.Write("Check difference with old file? y/n ");
            ConsoleKey response = Console.ReadKey().Key;
            if(response == ConsoleKey.Y)
            {
                Console.WriteLine();
                List<Ship> difference = gen.GetDifference(shipDataJsonUri, settings.FtpLogin);
                if(difference != null && difference.Count > 0)
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

            Console.Write(Environment.NewLine + "Generate new Json? y/n ");
            ConsoleKey response2 = Console.ReadKey().Key;
            Console.WriteLine();

            if (response2 == ConsoleKey.Y)
            {
                gen.MakeJson();

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
