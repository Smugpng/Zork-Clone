using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zork
{

    class Program
    {
        private static Room[,] Rooms;
        private static Room CurrentRoom
        {
            get
            {
                return Rooms[Location.Row, Location.Column];
            }
        }
        private enum CommandLineArguments
        {
            RoomsFilename = 0
        }
        static void Main(string[] args)
        {
            const string defaultRoomsFileName = "Rooms.json";
            string roomsFileName = (args.Length > 0 ? args[(int)CommandLineArguments.RoomsFilename] : defaultRoomsFileName);
                string roomsFilename = "Rooms.json";

            InitializeRoomDescriptions(roomsFilename);
            Console.WriteLine("Welcome to Zork!");

            Room previousRoom = null;
            if (previousRoom != CurrentRoom)
            {
                Console.WriteLine(CurrentRoom.Description);
                previousRoom = CurrentRoom;
            }
            Commands command = Commands.UNKOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(CurrentRoom);
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.QUIT:
                        Console.WriteLine("Thanks for playing!");
                        break;

                    case Commands.LOOK:
                        Console.WriteLine(CurrentRoom.Description);
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command) == false)
                        {
                            Console.WriteLine("The way is shut!");
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
        }
        private static bool Move(Commands command)
        {
            Assert.IsTrue(IsDirection(command), "Invalid direction.");


            bool isValidMove = true;
            switch (command)
            {
                case Commands.NORTH when Location.Row < Rooms.GetLength(0) - 1:
                    Location.Row++;
                    break;

                case Commands.SOUTH when Location.Row < Rooms.GetLength(0) - 1:
                    Location.Row--;
                    break;

                case Commands.EAST when Location.Column < Rooms.GetLength(0) - 1:
                    Location.Column++;
                    break;

                case Commands.WEST when Location.Column < Rooms.GetLength(0) - 1:
                    Location.Column--;
                    break;

                default:
                    isValidMove = false;
                    break;
            }
            return isValidMove;

        }
        
        

        private static Commands ToCommand(string commandString) => (Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKOWN);

        private static bool IsDirection(Commands command) => Directions.Contains(command);

        private static readonly List<Commands> Directions = new List<Commands>()
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };
        private static readonly Room[,] Rooms =
        {
            {new Room("Rocky Trail"),new Room("South Of House"), new Room("Canyon View") },
            {new Room("Forest"), new Room("West of House"), new Room("Behind House") },
            {new Room("Dense Woods"), new Room("North of House"), new Room("Clearing") }
        };
        private static void InitializeRoomDescriptions(string roomsFilename) => Rooms = JsonConvert.DeserializeObject<Room[,]>(File.ReadAllText(roomsFilename));
        private enum Fields
        {
            Name = 0,
            Description
        }

        private static (int Row, int Column) Location = (1, 1);
        private static Dictionary<string, Room> roomMap;
    }
}

