using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            string inputString = Console.ReadLine();
            inputString = inputString.ToUpper();
            if(inputString == "Quit")
            {
                Console.WriteLine("Thank you for playing.");
            }
            else if (inputString == "LOOK")
            {
                Console.WriteLine("This is an open field of a white house, with a boarded door. \nA rubber mat saying 'Welcoe to Zork!' lies by the door.");
            }
            else
            {
                Console.WriteLine("Unrecognized command");
            }
        }
    }
}
