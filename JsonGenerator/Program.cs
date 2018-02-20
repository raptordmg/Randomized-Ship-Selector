using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ship Json Generator Tool");
            RSSjsonMaker maker = new RSSjsonMaker(new string[] { "Cossack", "T-61", "Asashio", "Monaghan" });

            Console.WriteLine("Check difference with old file? y/n");
            string response = Console.ReadLine();
            if(response.Equals("y"))
            {
                Console.WriteLine("New Ships:");
                List<Ship> difference = maker.GetDifference();
                foreach (Ship s in difference)
                {
                    Console.WriteLine(s.ID + " - " + s.Name);
                }

                Console.WriteLine("Press enter to continue");
                Console.Read();
            }


            Console.WriteLine("Generate new Json? y/n");
            response = Console.ReadLine();

            if (response.Equals("y"))
            {
                maker.MakeJson();
            }

            Console.Read();
        }
    }
}
