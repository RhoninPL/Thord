using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Thord.Core.Extensions;
using Thord.Core.Interfaces;

namespace Thord.Core
{
    public class SynchronizeHandler
    {
        #region Fields

        private readonly ILogger _logger;
        private long _totalFilesSize, _copiedFilesSize;

        private int _totalFilesToCopy, _copiedFilesNumber;

        public List<string> FoldersSkip;
        public Action<double> PercentageProgressHanlder;
        public bool ShowErrors;

        #endregion

        #region Properties

        public IProgress<string> ProgressHanlder { get; set; }

        #endregion

        #region Constructors

        public SynchronizeHandler(ILogger logger)
        {
            _logger = logger;
        }

        #endregion

        #region Public Methods

        public async Task StartCopy(DirectoryInfo source, DirectoryInfo target)
        {
            await Task.Run(() =>
            {
                ReadAllFiles(source);
                _logger.LogInfo($"Files: {_copiedFilesNumber}/{_totalFilesToCopy}, _copiedFilesSize: {_copiedFilesSize}{_totalFilesSize}");
                CopyAll(source, target);
            }).ConfigureAwait(false);
        }

        #endregion

        #region  Private Methods

        private void ReadAllFiles(DirectoryInfo source)
        {
            try
            {
                var files = source.GetFiles();
                _totalFilesToCopy += files.Length;
                _totalFilesSize += files.Select(file => file.Length).Sum();
                foreach (var subDirectory in source.GetDirectories())
                {
                    if ((subDirectory.Attributes & FileAttributes.Hidden) != 0)
                        continue;

                    if (FoldersSkip?.Contains(subDirectory.FullName) == true)
                        continue;

                    ReadAllFiles(subDirectory);
                }
            }
            catch
            {
                if (ShowErrors)
                {
                    _logger.LogWarning($"Can not access to directory {source.Name}.");
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

                    if ((sourceDirectory.Attributes & FileAttributes.Hidden) != 0)
                        continue;

                    var nextTargetSubDir = target.CreateSubdirectory(sourceDirectory.Name);
                    CopyAll(sourceDirectory, nextTargetSubDir);
                }

                foreach (var targetDirectory in target.GetDirectories())
                {
                    var sourceFile = sourceDirecotries.FirstOrDefault(sFile => sFile.Name == targetDirectory.Name);
                    if (sourceFile != null)
                        continue;

                    _logger.LogInfo($"Deleting folder {targetDirectory.Name}");
                    targetDirectory.Delete(true);
                }
            }
            catch
            {
                if (ShowErrors)
                {
                    _logger.LogInfo($"Can not access to directory {source.Name}.");
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

                    _logger.LogInfo($@"Copying {target.FullName}\{sourceFile.Name}");
                    //sourceFile.CopyTo(Path.Combine(target.FullName, sourceFile.Name), true);
                    sourceFile.CopyTo(Path.Combine(target.FullName, sourceFile.Name), percentage => { PercentageProgressHanlder(percentage); });
                    UpdateTitle(1, sourceFile.Length);
                }

                foreach (var targetFile in targetFiles)
                {
                    var sourceFile = sourceFiles.FirstOrDefault(sFile => sFile.Name == targetFile.Name);
                    if (sourceFile != null)
                        continue;

                    _logger.LogInfo($"Deleting file {targetFile.Name}");
                    targetFile.Delete();
                }
            }
            catch
            {
                if (ShowErrors)
                {
                    _logger.LogWarning($"Can not access to directory {source.Name}.");
                }
            }
        }

        private void UpdateTitle(int files, long size)
        {
            _copiedFilesSize += size;
            var percentage = (float)_copiedFilesSize / _totalFilesSize;
            var quantity = _copiedFilesNumber += files;
            _logger.LogInfo($"Copied files: {quantity}/{_totalFilesToCopy}, {_copiedFilesSize / 1024f / 1024}/{_totalFilesSize / 1024f / 1024} MB. Progress: {percentage:P} ");
            ProgressHanlder?.Report($"Copied files: {quantity}/{_totalFilesToCopy}");
        }

        #endregion
    }
}