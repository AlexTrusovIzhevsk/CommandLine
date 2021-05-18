using System;
using System.IO;
using System.Linq;

namespace CommandLine
{
    public class Commander
    {
        public string CurrentDirectory { get; private set; }

        public Commander(string currentDirectory)
        {
            CurrentDirectory = currentDirectory.TrimEnd('\\');
        }

        public string[] DirCommand(string[] args = null)
        {
            if (!(args is null) && args.Length > 0) throw new NotImplementedException("Данная функциональность не реализована. ");

            var paths = GetFullPathsSystemEntries();
            return paths
                .Select(path => GetRelativePath(path))
                .ToArray();
        }

        private string[] GetFullPathsSystemEntries() => 
            Directory.GetFileSystemEntries(CurrentDirectory);

        public void CdCommand(string[] args)
        {
            if (args is null) throw new ArgumentNullException("Аргументы имеют значение null. ");
            if (args.Length > 1) throw new NotImplementedException("Данная функциональность не реализована. ");

            var newCurrentDirectory = args.First().TrimEnd('\\');
            if (Directory.Exists(newCurrentDirectory))
            {
                CurrentDirectory = newCurrentDirectory;
            }
            else if (DirCommand().Contains(newCurrentDirectory))
            {
                CurrentDirectory = GetFullPath(newCurrentDirectory); 
            }
            else
            {
                throw new DirectoryNotFoundException("Системе не удается найти указанный путь.");
            }
        }

        public void MkDirCommand(string[] args)
        {
            if (args is null) throw new ArgumentNullException("Аргументы имеют значение null. ");
            if (args.Length > 1) throw new NotImplementedException("Данная функциональность не реализована. ");

            var newDirectory = args.First();
            Directory.CreateDirectory(GetFullPath(newDirectory));
        }

        public void RmDirCommand(string[] args)
        {
            if (args is null) throw new ArgumentNullException("Аргументы имеют значение null. ");
            if (args.Length > 1) throw new NotImplementedException("Данная функциональность не реализована. ");

            var directory = args.First();
            if (GetFullPathsSystemEntries().Contains(directory))
            {
                Directory.Delete(directory);
            }
            else if(DirCommand().Contains(directory))
            {
                Directory.Delete(GetFullPath(directory));
            }
            else
            {
                throw new DirectoryNotFoundException("Системе не удается найти указанный путь.");
            }
        }

        public void MkFileCommand(string[] args)
        {
            if (args is null) throw new ArgumentNullException("Аргументы имеют значение null. ");
            if (args.Length > 1) throw new NotImplementedException("Данная функциональность не реализована. ");

            var newFilePath = args.First();
            var file = new FileInfo(newFilePath);
            var fileStream = file.Create();
            fileStream.Close();
        }


        public void RmFileCommand(string[] args)
        {
            if (args is null) throw new ArgumentNullException("Аргументы имеют значение null. ");
            if (args.Length > 1) throw new NotImplementedException("Данная функциональность не реализована. ");

            var file = args.First();
            var fullPath = GetFullPath(file);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            else if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            else
            {
                throw new FileNotFoundException("Системе не удается найти указанный путь.");
            }
            
        }

        private string GetFullPath(string relativePath) => CurrentDirectory + '\\' + relativePath;
        private string GetRelativePath(string fullPath) => fullPath.Replace(CurrentDirectory, "").Trim('\\');
    }
}
