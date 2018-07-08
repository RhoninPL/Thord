using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Thord.Core.Models
{
    public class BackupTask : INotifyPropertyChanged
    {
        #region Properties

        private string _sourceDirectory;
        public string SourceDirectory
        {
            get { return _sourceDirectory; }
            set
            {
                _sourceDirectory = value;
                OnPropertyChanged("SourceDirectory");
            }
        }

        private string _targetDirectory;
        public string TargetDirectory
        {
            get { return _targetDirectory; }
            set
            {
                _targetDirectory = value;
                OnPropertyChanged("TargetDirectory");
            }
        }
        public bool ShowErrors { get; set; }
        public bool DeleteDeletedFiles { get; set; }
        public List<string> FoldersToSkip { get; set; }
        public DateTime Creation { get; set; }
        public DateTime LastRun { get; set; }
        public bool OverwriteOldFiles { get; set; }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}