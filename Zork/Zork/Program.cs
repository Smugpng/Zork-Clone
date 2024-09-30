using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zork
{

    class Program
    {
        public int PlayerPos = 1;
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
                    case Commands.QUIT:
                        outputString = "Thanks for playing!";
                        break;
                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        //Move();
                        outputString = $"You moved {command}.";
                        break;

                    case Commands.UNKOWN:
                        outputString = "HELLO?";
                        break;
                    case Commands.LOOK:
                        outputString = "This is an open field west of a white house, with a boarded front door. \nA rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;

                    default:
                        outputString = "Unknown command.";
                        break;

                }

                Console.WriteLine(outputString);

            }
        }

        private static Commands ToCommand(string commandString) => (Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKOWN);

        private String[] rooms = new string[] { "Forest", "West of House", "Behind House", "Clearing", "Canyon View" };


        private bool Move(Commands command) 
        {
            switch (command)
            {
                case Commands.NORTH:
                case Commands.SOUTH:
                case Commands.EAST:
                    PlayerPos += 1;
                    break;
                case Commands.WEST:
                    PlayerPos -= 1;
                    break;  
                default:
                    return false;
            }
            return true;
        }
    }
}

