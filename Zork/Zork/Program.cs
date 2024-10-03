using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            const string defaultGameFilename = "Zork.json";
            string gameFilename = (args.Length > 0 ? args[(int)CommandLineArguments.GameFilename] : defaultGameFilename);

            Game.Start(gameFilename);
            Console.WriteLine("Thank you for Playing!");
        }
        private enum CommandLineArguments
        {
            GameFilename = 0
        }
    }
}

