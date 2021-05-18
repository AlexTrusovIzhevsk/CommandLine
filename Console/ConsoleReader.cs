using System;

namespace CommandLine
{
    public class ConsoleReader
    {
        private readonly Commander commander;

        public ConsoleReader(Commander commander)
        {
            this.commander = commander;
        }

        public void Work()
        {
            System.Console.WriteLine("(c)Трусов Алексей. Реализация командной строки");
            while (true)
            {
                try
                {
                    System.Console.Write(commander.CurrentDirectory + '>');
                    var textFullCommand = System.Console.ReadLine();
                    var commandWithArgs = CommandWithArgs.Create(textFullCommand);
                    var exitFlag = false;

                    switch (commandWithArgs.Command)
                    {
                        case Command.dir:
                            foreach (var name in commander.DirCommand()) System.Console.WriteLine(name);
                            break;

                        case Command.mkdir:
                            commander.MkDirCommand(commandWithArgs.Args);
                            break;

                        case Command.rmdir:
                            commander.RmDirCommand(commandWithArgs.Args);
                            break;

                        case Command.cd:
                            commander.CdCommand(commandWithArgs.Args);
                            break;

                        case Command.mkfile:
                            commander.MkFileCommand(commandWithArgs.Args);
                            break;

                        case Command.rmfile:
                            commander.RmFileCommand(commandWithArgs.Args);
                            break;

                        case Command.exit:
                            exitFlag = true;
                            break;
                    }
                    if (exitFlag) break;
                }
                catch (Exception exception)
                {
                    System.Console.WriteLine(exception.Message);
                }

                System.Console.WriteLine();
            }
        }
    }
}
