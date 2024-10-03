using System;
using System.Collections;
using System.Collections.Generic;

namespace Zork
{
    [CommandClassAtribute]
    public static class QuitCommand
    {
        [Command("QUIT", new string[] {"QUIT", "Q", "GOODBYE", "BYE"})]
        public static void Qui(Game game, CommandContext commandContext)
        {
            if(game.ConfirmAction("Are you sure you want to quit?"))
            {
                game.Quit();
            }
        }
    }
    
}