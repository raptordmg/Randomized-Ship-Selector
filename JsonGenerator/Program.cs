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
            RSSjsonMaker maker = new RSSjsonMaker();

            Console.WriteLine("Check difference with old file? y/n");
            ConsoleKey response = Console.ReadKey().Key;
            if(response == ConsoleKey.Y)
            {
                Console.WriteLine();
                Console.WriteLine("New Ships:");
                List<Ship> difference = maker.GetDifference();
                foreach (Ship s in difference)
                {
                    Console.WriteLine(s.ID + " - " + s.Name);
                }
            }

            Console.WriteLine(Environment.NewLine + "Generate new Json? y/n");
            ConsoleKey response2 = Console.ReadKey().Key;
            Console.WriteLine();

            if (response2 == ConsoleKey.Y)
            {
                maker.MakeJson();
            }

            Console.WriteLine("Finished!");
            Console.ReadLine();
        }
    }
}
