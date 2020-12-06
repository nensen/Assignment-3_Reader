using System;
using System.IO;

namespace Reader
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var filePath = GetFilePath();

            var fileContent = GetFileContent(filePath);

            if (String.IsNullOrEmpty(fileContent))
            {
                return;
            }

            var isFileEncrypted = IsFileEncrypted();

            if (isFileEncrypted)
            {
                ICryptor cryptor = new ReverseCryptor();
                fileContent = cryptor.Decrypt(fileContent);
            }

            DumpFileContentToScreen(fileContent);

            Console.ReadKey();
        }

        private static bool IsFileEncrypted()
        {
            Console.WriteLine("Is file encrypted? type: Y for yes, N for no");
            var key = Console.ReadLine();
            return key == "Y";
        }

        private static void DumpFileContentToScreen(string text)
        {
            Console.Clear();
            Console.WriteLine("File content bellow");
            Console.WriteLine();
            Console.WriteLine(text);
        }

        private static string GetFileContent(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Specified file do not exist in directory {GetFileDir()}");
                Console.WriteLine("Exiting application.");
                return string.Empty;
            }

            Console.WriteLine($"Reading file in location: {filePath}");
            return File.ReadAllText(filePath);
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