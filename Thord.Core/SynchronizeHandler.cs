using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Thord.Core
{
    // TODO : create logger interface
    public class SynchronizeHandler
    {
        #region Public Fields

        public List<string> FoldersSkip;
        public bool ShowErrors;

        #endregion

        #region Private Fields

        private int _totalFilesToCopy, _copiedFilesNumber;
        private long _totalFilesSize, _copiedFilesSize;

        #endregion

        #region Public Methods

        public void StartCopy(DirectoryInfo source, DirectoryInfo target)
        {
            ReadAllFiles(source);
            Console.Title = $"Files: {_copiedFilesNumber}/{_totalFilesToCopy}, _copiedFilesSize: {_copiedFilesSize}{_totalFilesSize}";
            CopyAll(source, target);
        }

        #endregion

        #region Private Methods

        private void ReadAllFiles(DirectoryInfo source)
        {
            try
            {
                var files = source.GetFiles();
                _totalFilesToCopy += files.Length;
                _totalFilesSize += files.Select(file => file.Length).Sum();
                foreach (var subDirectory in source.GetDirectories())
                {
                    if (subDirectory.Attributes.HasFlag(FileAttributes.Hidden))
                        continue;

                    if (FoldersSkip?.Contains(subDirectory.FullName) == true)
                    {
                        continue;
                    }

                    ReadAllFiles(subDirectory);

                }
            }
            catch
            {
                if (ShowErrors)
                {
                    Console.WriteLine($"Can not access to directory {source.Name}.");
                }
            }
        }

        private void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);
            CompareFilesToCopy(source, target);

            CopySubdir(source, target);
        }

        private void CopySubdir(DirectoryInfo source, DirectoryInfo target)
        {

            try
            {
                var sourceDirecotries = source.GetDirectories();
                foreach (var sourceDirectory in sourceDirecotries)
                {
                    if (FoldersSkip != null)
                    {
                        if (FoldersSkip.Contains(sourceDirectory.FullName))
                            continue;
                    }

                    if (sourceDirectory.Attributes.HasFlag(FileAttributes.Hidden))
                        continue;

                    var nextTargetSubDir = target.CreateSubdirectory(sourceDirectory.Name);
                    CopyAll(sourceDirectory, nextTargetSubDir);
                }

                foreach (var targetDirectory in target.GetDirectories())
                {
                    var sourceFile = sourceDirecotries.FirstOrDefault(sFile => sFile.Name == targetDirectory.Name);
                    if (sourceFile != null)
                        continue;

                    Console.WriteLine($"Deleting folder {targetDirectory.Name}");
                    targetDirectory.Delete(true);
                }
            }
            catch
            {
                if (ShowErrors)
                {
                    Console.WriteLine($"Can not access to directory {source.Name}.");
                }
            }
        }

        private void CompareFilesToCopy(DirectoryInfo source, DirectoryInfo target)
        {
            try
            {
                var sourceFiles = source.GetFiles();
                var targetFiles = target.GetFiles().ToList();

                foreach (var sourceFile in sourceFiles)
                {
                    var targetFile = targetFiles.Find(tFile => sourceFile.Name == tFile.Name);
                    if (targetFile != null && targetFile.CreationTime >= sourceFile.CreationTime)
                    {
                        UpdateTitle(1, sourceFile.Length);
                        continue;
                    }
                    
                    Console.WriteLine(@"Copying {0}\{1}", target.FullName, sourceFile.Name);
                    sourceFile.CopyTo(Path.Combine(target.FullName, sourceFile.Name), true);
                    UpdateTitle(1, sourceFile.Length);
                }

                foreach (var targetFile in targetFiles)
                {
                    var sourceFile = sourceFiles.FirstOrDefault(sFile => sFile.Name == targetFile.Name);
                    if(sourceFile != null)
                        continue;

                    Console.WriteLine($"Deleting file {targetFile.Name}");
                    targetFile.Delete();
                }
            }
            catch
            {
                if (ShowErrors)
                {
                    Console.WriteLine($"Can not access to directory {source.Name}.");
                }
            }
        }

        private void UpdateTitle(int files, long size)
        {
            _copiedFilesSize += size;
            var percentage = (float)_copiedFilesSize / _totalFilesSize;
            Console.Title = $"Copied files: {_copiedFilesNumber += files}/{_totalFilesToCopy}, {(_copiedFilesSize / 1024f) / 1024}/{(_totalFilesSize / 1024f) / 1024} MB. Progress: {percentage:P} ";
        }

        #endregion
    }
}