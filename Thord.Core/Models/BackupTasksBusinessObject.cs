using System;
using System.Collections.Generic;

namespace Thord.Core.Models
{
    public class BackupTasksBusinessObject
    {
        #region Properties

        private List<BackupTask> _backupTasks { get; }

        public event EventHandler BackupTaskChanged;

        #endregion

        #region Constructors

        public BackupTasksBusinessObject()
        {
            _backupTasks = new List<BackupTask>
            {
                new BackupTask { SourceDirectory = "C:\\", TargetDirectory = "D:\\" },
                new BackupTask { SourceDirectory = "E:\\", TargetDirectory = "D:\\" },
                new BackupTask { SourceDirectory = "F:\\", TargetDirectory = "D:\\", OverwriteOldFiles = true }
            };
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
            OnBackupTaskChanged();
        }

        public void DeleteBackupTask(BackupTask backupTask)
        {
            _backupTasks.Remove(backupTask);
            OnBackupTaskChanged();
        }

        public void UpdateBackupTask(BackupTask backupTask)
        {
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