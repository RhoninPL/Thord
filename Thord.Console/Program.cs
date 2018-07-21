using System.Collections.Generic;
using System.IO;
using Thord.Core;

namespace Thord.Console
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args)
        {
            System.Console.Write("Enter source folder: ");
            var sourceFolderName = System.Console.ReadLine();
            System.Console.Write("Enter target folder: ");
            var targetFolderName= System.Console.ReadLine();

            System.Console.Write("Enter folder to skip: ");
            var folderToSkip= System.Console.ReadLine();


            var sourceFolder = new DirectoryInfo(sourceFolderName);
            var targetFolder = new DirectoryInfo(targetFolderName);

            // TODO: implement ILogger class
            //var copyHandler = new SynchronizeHandler()
            //{
            //    FoldersSkip = new List<string>() { folderToSkip }
            //};
            //copyHandler.StartCopy(sourceFolder, targetFolder);

            System.Console.WriteLine("Synchronize done. Press enter to exit...");
            System.Console.ReadLine();
        }

        #endregion
    }
}