using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Thord.Core.Configuration;

namespace Thord.Core.Models
{
    public class BackupTasksBusinessObject
    {
        #region Properties

        private List<BackupTask> _backupTasks { get; }

        public event EventHandler BackupTaskChanged;

        private IBackupTasksFileHandler fileHandler;

        #endregion

        #region Constructors

        public BackupTasksBusinessObject()
        {
            this.fileHandler = new BackupTasksFileHandler();
            //_backupTasks = new List<BackupTask>
            //{
            //    new BackupTask { SourceDirectory = "C:\\", TargetDirectory = "D:\\" },
            //    new BackupTask { SourceDirectory = "E:\\", TargetDirectory = "D:\\" },
            //    new BackupTask { SourceDirectory = "F:\\", TargetDirectory = "D:\\", OverwriteOldFiles = true }
            //};
            _backupTasks = fileHandler.ReadFile().ToList();
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
            _backupTasks.Add(backupTask);
            fileHandler.SaveFile(_backupTasks);
            OnBackupTaskChanged();
        }

        public void DeleteBackupTask(BackupTask backupTask)
        {
            _backupTasks.Remove(backupTask);
            fileHandler.SaveFile(_backupTasks);
            OnBackupTaskChanged();
        }

        public void UpdateBackupTask(BackupTask backupTask)
        {
            var oldTask = _backupTasks.Find(t => t.Id == backupTask.Id);
            oldTask = backupTask;
            fileHandler.SaveFile(_backupTasks);
        }

        #endregion

        #region  Private Methods

        private void OnBackupTaskChanged()
        {
            BackupTaskChanged?.Invoke(this, null);
        }

        #endregion
    }
}