using System;
using System.Collections.Generic;

namespace Thord.Core.Models
{
    public class Task
    {
        #region Properties

        public string SourceDirectory { get; set; }
        public string TargetDirectory { get; set; }
        public bool ShowErrors { get; set; }
        public bool DeleteDeletedFiles { get; set; }
        public List<string> FoldersToSkip { get; set; }
        public DateTime Creation { get; set; }
        public DateTime LastRun { get; set; }

        #endregion
    }
}