using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zork
{
    public class CommandManger
    {
        public CommandManger() => mCommands = new HashSet<Command>();

        public CommandManger(IEnumerable<Command> commands) => mCommands = new HashSet<Command>(commands);
        
        public CommandContext Parse(string commandString)
        {
            var commnadQuery = from command in mCommands
                               where command.Verbs.Contains(commandString,StringComparer.OrdinalIgnoreCase)
                               select new CommandContext(commandString, command);

            return commnadQuery.FirstOrDefault();
        }
        public bool PerformCommand(Game game, string commandString)
        {
            bool result;
            CommandContext commandContext = Parse(commandString);
            if (commandContext.Command != null)
            {
                commandContext.Command.Action(game, commandContext);
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public void AddCommand(Command command) => mCommands.Add(command);

        public void RemoveCommand(Command command) => mCommands.Remove(command);

        public void Addcommnds(IEnumerable<Command> commands) => mCommands.UnionWith(commands);

        public void ClearCommands() => mCommands.Clear();

        private HashSet<Command> mCommands;
    }
}