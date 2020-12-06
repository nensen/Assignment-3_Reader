using System;
using System.IO;

namespace Reader
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var filePath = GetFilePath();
            ReadAndDumpFile(filePath);

            Console.ReadKey();
        }

        private static void ReadAndDumpFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Specified file do not exist in directory {GetFileDir()}");
                Console.WriteLine("Exiting application.");
                return;
            }

            Console.WriteLine($"Reading file in location: {filePath}");
            string text = File.ReadAllText(filePath);

            Console.WriteLine("File content bellow");
            Console.WriteLine();
            Console.WriteLine(text);
        }

        private static string GetFilePath()
        {
            var fileDir = GetFileDir();
            Console.Write($"Please provide valid file name in location {fileDir}");
            Console.WriteLine();
            var fileName = Console.ReadLine();

            Console.Write($"Please provide file extension (e.g. .xml or .txt)");
            Console.WriteLine();
            var fileExtension = Console.ReadLine();
            return Path.Combine(fileDir, fileName + fileExtension);
        }

        private static string GetFileDir()
        {
            string workingDir = Environment.CurrentDirectory;
            string projectDir = Directory.GetParent(workingDir).Parent.Parent.Parent.FullName;
            return Path.Combine(projectDir, "Files");
        }
    }
}