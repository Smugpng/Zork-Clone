using System;
using Zork;

string[] response = new string[]
{
    "Good day.",
    "Nice weather we've been having lately.",
    "Nice to see you."
};

var command = new Command("HELLO", new string[] { "HELLO", "HI", "HOWDY" },
    (game, commandContext) =>
    {
        string selectedResponse = response[Game.Random.Next(response.Length)];
        Console.WriteLine(selectedResponse);
    });

Game.Instance.CommandManager.AddCommand(command);
