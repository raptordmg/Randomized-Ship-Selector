using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonGenerator
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ship Json Generator Tool");
            Generator gen = new Generator();

            gen.PrintIgnoredShips();

            Console.Write("Check difference with old file? y/n ");
            ConsoleKey response = Console.ReadKey().Key;
            if(response == ConsoleKey.Y)
            {
                Console.WriteLine();
                Console.WriteLine("New Ships:");
                List<Ship> difference = gen.GetDifference();
                foreach (Ship s in difference)
                {
                    Console.WriteLine(s.ID + " - " + s.Name);
                }
            }

            Console.Write(Environment.NewLine + "Generate new Json? y/n ");
            ConsoleKey response2 = Console.ReadKey().Key;
            Console.WriteLine();

            if (response2 == ConsoleKey.Y)
            {
                gen.MakeJson();
            }

            Console.WriteLine("Finished!");
            Console.ReadLine();
        }
    }
}
