using System.IO;

namespace CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var commander = new Commander(currentDirectory);
            var cosoleReader = new ConsoleReader(commander);
            
            cosoleReader.Work();
        }
    }
}
