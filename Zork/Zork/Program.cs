using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zork
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKOWN;
            while (command != Commands.QUIT)
            {
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString;
                switch (command)
                {
                    case Commands.NORTH:
                        outputString = "You Moved NORTH";
                        break;

                    case Commands.SOUTH:
                        outputString = "You moved SOUTH";
                        break;

                    case Commands.EAST:
                        outputString = "You moved EAST";
                        break;

                    case Commands.WEST:
                        outputString = "You moved WEST";
                        break;

                    case Commands.UNKOWN:
                        outputString = "HELLO?";
                        break;
                    case Commands.LOOK:
                        outputString = "A Rubber mat saying 'Welcome To Zork!' lies by the door.";
                        break;

                    default:
                        outputString = "Thanks For Playing";
                        break ;
                     
                }

                Console.WriteLine(outputString);

            }   
        }

        private static Commands ToCommand(string commandString) => (Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKOWN);
    }
}

