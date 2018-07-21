using System;
using System.Collections.Generic;
using System.Linq;
using Thord.Core.Configuration;
using Thord.Core.Interfaces;

namespace Thord.Core.Models
{
    public class BackupTasksBusinessObject
    {
        #region Fields

        private readonly IBackupTasksFileHandler _fileHandler;

        #endregion

        #region Properties

        private List<BackupTask> _backupTasks { get; }

        #endregion

        #region Constructors

        public BackupTasksBusinessObject()
        {
            _fileHandler = new BackupTasksFileHandler();
            //_backupTasks = new List<BackupTask>
            //{
            //    new BackupTask { SourceDirectory = "C:\\", TargetDirectory = "D:\\" },
            //    new BackupTask { SourceDirectory = "E:\\", TargetDirectory = "D:\\" },
            //    new BackupTask { SourceDirectory = "F:\\", TargetDirectory = "D:\\", OverwriteOldFiles = true }
            //};
            var backupTasks = _fileHandler.ReadFile()?.ToList();
            _backupTasks = backupTasks ?? new List<BackupTask>();
            //_backupTasks.CollectionChanged += (sender, args) => fileHandler.SaveFile(_backupTasks);
        }

        #endregion

        #region Public Methods

        public List<BackupTask> GetBackupTasks()
        {
            return _backupTasks;
        }

        public void AddBackupTask(BackupTask backupTask)
        {
            backupTask.Id = _backupTasks.Any() ? _backupTasks.Max(task => task.Id) + 1 : 1;
            backupTask.Creation = DateTime.Now;
            _backupTasks.Add(backupTask);
            _fileHandler.SaveFile(_backupTasks);
            OnBackupTaskChanged();
        }

        public void DeleteBackupTask(BackupTask backupTask)
        {
            _backupTasks.Remove(backupTask);
            _fileHandler.SaveFile(_backupTasks);
            OnBackupTaskChanged();
        }

        public void UpdateBackupTask(BackupTask backupTask)
        {
            var oldTask = _backupTasks.Find(t => t.Id == backupTask.Id);
            oldTask = backupTask;
            _fileHandler.SaveFile(_backupTasks);
        }

        #endregion

        #region  Private Methods

        private void OnBackupTaskChanged()
        {
            BackupTaskChanged?.Invoke(this, null);
        }

        #endregion

        public event EventHandler BackupTaskChanged;
    }
}