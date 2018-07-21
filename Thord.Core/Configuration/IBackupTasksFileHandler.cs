﻿using System.Collections.Generic;
using Thord.Core.Models;

namespace Thord.Core.Configuration
{
    public interface IBackupTasksFileHandler
    {
        #region Public Methods

        void SaveFile(IEnumerable<BackupTask> backupTasks);

        IEnumerable<BackupTask> ReadFile();

        #endregion
    }
}