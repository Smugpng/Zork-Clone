using System;
using System.Collections;
using System.Collections.Generic;

namespace Zork
{
    [CommandClassAtribute]
    public static class RestartCommand
    {
        [Command("RESTART", "RESTART")]
        public static void Restart(Game game, CommandContext commandContext)
        {
            if(game.ConfirmAction("Are you sure you want to restart?"))
            {
                game.Restart();
            }
                
        }
    }
    
}