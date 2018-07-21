using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Thord.Core.Models
{
    public class BackupTask : INotifyPropertyChanged
    {
        #region Fields

        private DateTime _creation;
        private bool _deleteDeletedFiles;
        private List<string> _foldersToSkip;
        private int _id;
        private DateTime _lastRun;

        private bool _overwriteOldFiles;
        private bool _showErrors;

        private string _sourceDirectory;

        private string _targetDirectory;

        #endregion

        #region Properties

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

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

        public bool ShowErrors
        {
            get
            {
                return _showErrors;
            }
            set
            {
                _showErrors = value;
                OnPropertyChanged("ShowErrors");
            }
        }

        public bool DeleteDeletedFiles
        {
            get
            {
                return _deleteDeletedFiles;
            }
            set
            {
                _deleteDeletedFiles = value;
                OnPropertyChanged("DeleteDeletedFiles");
            }
        }

        public List<string> FoldersToSkip
        {
            get
            {
                return _foldersToSkip;
            }
            set
            {
                _foldersToSkip = value;
                OnPropertyChanged("FoldersToSkip");
            }
        }

        public DateTime Creation
        {
            get
            {
                return _creation;
            }
            set
            {
                _creation = value;
                OnPropertyChanged("Creation");
            }
        }

        public DateTime LastRun
        {
            get
            {
                return _lastRun;
            }
            set
            {
                _lastRun = value;
                OnPropertyChanged("LastRun");
            }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}