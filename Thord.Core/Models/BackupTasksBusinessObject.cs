using System;
using System.Collections.Generic;

namespace Thord.Core.Models
{
    public class BackupTasksBusinessObject
    {
        private List<BackupTask> _backupTasks { get; set; }
        public event EventHandler BackupTaskChanged;

        public BackupTasksBusinessObject()
        {
            _backupTasks = new List<BackupTask>
            {
                new BackupTask { SourceDirectory = "C:\\", TargetDirectory = "D:\\" },
                new BackupTask { SourceDirectory = "E:\\", TargetDirectory = "D:\\" },
                new BackupTask { SourceDirectory = "F:\\", TargetDirectory = "D:\\" }
            };
        }

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

        private void OnBackupTaskChanged()
        {
            if (BackupTaskChanged != null)
                BackupTaskChanged(this, null);
        }
    }
}