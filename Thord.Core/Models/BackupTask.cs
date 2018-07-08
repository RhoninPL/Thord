using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Thord.Core.Models
{
    public class BackupTask : INotifyPropertyChanged
    {
        #region Fields

        private bool _overwriteOldFiles;

        private string _sourceDirectory;

        private string _targetDirectory;

        #endregion

        #region Properties

        public string SourceDirectory
        {
            get
            {
                return _sourceDirectory;
            }
            set
            {
                _sourceDirectory = value;
                OnPropertyChanged("SourceDirectory");
            }
        }

        public string TargetDirectory
        {
            get
            {
                return _targetDirectory;
            }
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

        public bool OverwriteOldFiles
        {
            get
            {
                return _overwriteOldFiles;
            }
            set
            {
                _overwriteOldFiles = value;
                OnPropertyChanged("OverwriteOldFiles");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region  Protected Methods

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}