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
            RSSjsonMaker maker = new RSSjsonMaker(@"C:\Users\Daan\Source\Repos\Randomized-Ship-Selector\Randomized Ship Selector\Resources\Panzerschiffer_Icons\");
            maker.MakeJson();
        }
    }
}
