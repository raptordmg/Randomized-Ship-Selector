using System;
using System.Collections.Generic;
using System.Linq;
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
            public string FtpUrl { get; }
            public string UserName { get; }
            public string UserPassword { get; }

            public Settings(string appId, string ftpUrl, string userName, string userPassword)
            {
                AppID = appId;
                FtpUrl = ftpUrl;
                UserName = userName;
                UserPassword = userPassword;
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
                config.DocumentElement.SelectSingleNode("ftpData/userName").InnerText,
                config.DocumentElement.SelectSingleNode("ftpData/userPassword").InnerText);
                
            Generator gen = new Generator(settings.AppID);

            gen.PrintIgnoredShips();

            Console.Write("Check difference with old file? y/n ");
            ConsoleKey response = Console.ReadKey().Key;
            if(response == ConsoleKey.Y)
            {
                Console.WriteLine();
                List<Ship> difference = gen.GetDifference();
                if (difference == null)
                {
                    Console.WriteLine("No existing file was found.");
                }
                else if (difference.Count == 0)
                {
                    Console.WriteLine("No new ships found.");
                }
                else
                { 
                    Console.WriteLine("New Ships:");
                    foreach (Ship s in difference)
                    {
                        Console.WriteLine(s.ID + " - " + s.Name);
                    }
                }
            }

            Console.Write(Environment.NewLine + "Generate new Json? y/n ");
            ConsoleKey response2 = Console.ReadKey().Key;
            Console.WriteLine();

            if (response2 == ConsoleKey.Y)
            {
                gen.MakeJson();
            }

            Console.WriteLine();
            Console.Write("Upload to server? y/n ");
            ConsoleKey response3 = Console.ReadKey().Key;
            Console.WriteLine();

            if (response3 == ConsoleKey.Y)
            {
                gen.UploadJson(settings.FtpUrl, settings.UserName, settings.UserPassword);
            }

            Console.WriteLine("Press <Enter> to exit.");
            Console.ReadLine();
        }
    }
}
