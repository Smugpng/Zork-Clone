using System;
using System.Collections;
using System.Collections.Generic;

namespace Zork
{
    [CommandClassAtribute]
    public static class LookCommand
    {
        [Command("LOOK", new string[] {"Look", "L"})]
        public static void Look(Game game, CommandContext commandContext) => Console.WriteLine(game.Player.Location.Description);
    }
    
}