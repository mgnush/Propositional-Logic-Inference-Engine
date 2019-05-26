using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ai_ass2
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = File.ReadAllText(args[1]);
            Engine engine;
            switch (args[0])
            {
                case "TT":
                    engine = new TTEngine(file);
                    break;
                case "BC":
                    engine = new BCEngine(file);
                    break;
                case "FC":
                    engine = new FCEngine(file);
                    break;
                default:
                    engine = new TTEngine(file);
                    Console.WriteLine("{0} is not a method", args[0]);
                    return;
            }

            Console.WriteLine(engine.KbEntails());
        }
    }
}
