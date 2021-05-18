using System;
using System.Linq;

namespace CommandLine
{
    public class CommandWithArgs
    {
        public readonly Command Command;
        public readonly string[] Args;

        private CommandWithArgs(Command command, string[] args)
        {
            Command = command;
            Args = args;
        }

        public static CommandWithArgs Create(string commandText)
        {
            var commandParts = commandText.Split();
            if (commandParts.Any() && Enum.TryParse(commandParts.First().ToLower(), out Command command))
                return new CommandWithArgs(command, commandParts.Skip(1).ToArray());

            throw new ArgumentException($"{commandText} it is uncorrected command");
        }
    }
}
