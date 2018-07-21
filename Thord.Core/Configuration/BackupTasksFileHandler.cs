using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Thord.Core.Models;

namespace Thord.Core.Configuration
{
    public sealed class BackupTasksFileHandler : IBackupTasksFileHandler
    {
        #region Constants

        private const string ConfigFileName = "configuration.json";

        #endregion

        #region Public Methods

        public void SaveFile(IEnumerable<BackupTask> backupTasks)
        {
            var serialized = JsonConvert.SerializeObject(backupTasks);
            File.WriteAllText(ConfigFileName, serialized);
        }

        public IEnumerable<BackupTask> ReadFile()
        {
            if (!File.Exists(ConfigFileName))
                File.Create(ConfigFileName);

            var content = File.ReadAllText(ConfigFileName);
            return JsonConvert.DeserializeObject<IEnumerable<BackupTask>>(content);
        }

        #endregion
    }
}