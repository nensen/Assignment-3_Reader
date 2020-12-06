using System;
using System.IO;

namespace Reader
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var isAdmin = IsUserAdmin();
            Console.Clear();

            var fileName = GetFileName();
            Console.Clear();

            var filePath = GetFilePath(fileName, isAdmin);
            Console.Clear();

            var fileContent = GetFileContent(filePath);
            Console.Clear();

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

            Console.Clear();
            DumpFileContentToScreen(fileContent);

            Console.ReadKey();
        }

        private static bool IsUserAdmin()
        {
            Console.WriteLine("Admin have access to admin folder. Are you admin? Type Y for Yes, N for no.");
            return Console.ReadLine() == "Y";
        }

        private static bool IsFileEncrypted()
        {
            Console.WriteLine("Is file encrypted? type: Y for yes, N for no");
            var key = Console.ReadLine();
            return key == "Y";
        }

        private static void DumpFileContentToScreen(string text)
        {
            Console.WriteLine("File content bellow:");
            Console.WriteLine();
            Console.WriteLine(text);
        }

        private static string GetFilePath(string fileName, bool isAdmin)
        {
            var userFilePath = Path.Combine(GetFileDir(), fileName);

            if (!File.Exists(userFilePath))
            {
                var adminFilePath = Path.Combine(GetFileDir(), "AdminFolder", fileName);

                if (!File.Exists(adminFilePath))
                {
                    Console.WriteLine($"Specified file do not exist in directory {GetFileDir()}");
                    Console.WriteLine("Exiting application.");
                    return string.Empty;
                }

                return adminFilePath;
            }

            return userFilePath;
        }

        private static string GetFileContent(string filePath)
        {
            Console.WriteLine($"Reading file in location: {filePath}");
            return File.ReadAllText(filePath);
        }

        private static string GetFileName()
        {
            var fileDir = GetFileDir();
            Console.Write($"Please provide valid file name in location {fileDir}");
            Console.WriteLine();
            var fileName = Console.ReadLine();

            Console.Write($"Please provide file extension (e.g. .xml or .txt)");
            Console.WriteLine();
            var fileExtension = Console.ReadLine();

            return fileName + fileExtension;
        }

        private static string GetFileDir()
        {
            string workingDir = Environment.CurrentDirectory;
            string projectDir = Directory.GetParent(workingDir).Parent.Parent.Parent.FullName;
            return Path.Combine(projectDir, "Files");
        }
    }
}