using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using Thord.App.Helpers;
using Thord.Core;
using Thord.Core.Models;

namespace Thord.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private readonly BackupTasksBusinessObject _backupTasksBusinessObject;

        private ObservableCollection<BackupTask> _backupTasks;
        private string _logs;
        private string _progress;
        private object _selectedBackupTask;

        BindingGroup _UpdateBindingGroup;
        private double _percentage;

        #endregion

        #region Properties

        public string Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                _progress = value;
                RaisePropertyChanged("Progress");
            }
        }

        public double Percentage
        {
            get
            {
                return _percentage;
            }
            set
            {
                _percentage = value;
                RaisePropertyChanged("Percentage");
            }
        }

        public ObservableCollection<BackupTask> BackupTasks
        {
            get
            {
                _backupTasks = new ObservableCollection<BackupTask>(_backupTasksBusinessObject.GetBackupTasks());
                return _backupTasks;
            }
        }

        public RelayCommand OpenAddTaskCommand { get; set; }

        public RelayCommand StartBackupCommand { get; set; }

        public RelayCommand AddBackupTaskCommand { get; set; }

        public RelayCommand SaveBackupTaskCommand { get; set; }

        public RelayCommand SelectSourceDirectoryCommand { get; set; }

        public RelayCommand RemoveBackupTaskCommand { get; set; }

        public string Logs
        {
            get
            {
                return _logs;
            }
            set
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(value);
                stringBuilder.AppendLine();
                stringBuilder.Append(_logs);
                _logs = stringBuilder.ToString();
                RaisePropertyChanged("Logs");
            }
        }

        public int SelectedIndex { get; set; }

        public object SelectedBackupTask
        {
            get
            {
                return _selectedBackupTask;
            }
            set
            {
                if (_selectedBackupTask != value)
                {
                    _selectedBackupTask = value;
                    RaisePropertyChanged("SelectedBackupTask");
                }
            }
        }

        public BindingGroup UpdateBindingGroup
        {
            get
            {
                return _UpdateBindingGroup;
            }
            set
            {
                if (_UpdateBindingGroup != value)
                {
                    _UpdateBindingGroup = value;
                    RaisePropertyChanged("UpdateBindingGroup");
                }
            }
        }

        #endregion

        #region Constructors

        #region Constructor

        public MainViewModel()
        {
            _backupTasksBusinessObject = new BackupTasksBusinessObject();
            _backupTasksBusinessObject.BackupTaskChanged += BackupTaskChanged;

            UpdateBindingGroup = new BindingGroup { Name = "Group1" };

            OpenAddTaskCommand = new RelayCommand(OpenAddTaskWindow);
            StartBackupCommand = new RelayCommand(StartBackup);
            AddBackupTaskCommand = new RelayCommand(AddBackupTask);
            SaveBackupTaskCommand = new RelayCommand(SaveBackupTask);
            SelectSourceDirectoryCommand = new RelayCommand(SelectSourceDirectory);
            RemoveBackupTaskCommand = new RelayCommand(RemoveBackupTask);

            SelectedBackupTask = null;
        }

        #endregion

        #endregion

        #region Public Methods

        public void RemoveBackupTask(object parameter)
        {
            var backup = SelectedBackupTask as BackupTask;
            _backupTasksBusinessObject.DeleteBackupTask(backup);
            RaisePropertyChanged("BackupTasks");
        }

        public void SelectSourceDirectory(object parameter)
        {
            var selectFolder = new CreatingTask
            {
                DataContext = new TaskViewModel()
            };
            selectFolder.Show();
        }

        public async void StartBackup(object parameter)
        {
            var progressHandler = new Progress<string>(value =>
                Progress = value
            );
            var progress = progressHandler as IProgress<string>;
            var synchronizeHanlder = new SynchronizeHandler(new WpfLogger(LogMessage));
            var backupTask = SelectedBackupTask as BackupTask;
            synchronizeHanlder.FoldersSkip = backupTask.FoldersToSkip;
            synchronizeHanlder.ShowErrors = backupTask.ShowErrors;
            synchronizeHanlder.ProgressHanlder = progress;
            synchronizeHanlder.PercentageProgressHanlder = percentage => { Percentage = percentage; };

            var sourceFolder = new DirectoryInfo(backupTask.SourceDirectory);
            var targetFolder = new DirectoryInfo(backupTask.TargetDirectory);

            await synchronizeHanlder.StartCopy(sourceFolder, targetFolder);
        }

        public void OpenAddTaskWindow(object parameter)
        {
            new CreatingTask
            {
                DataContext = this
            }.Show();
        }

        public void AddBackupTask(object parameter)
        {
            SelectedBackupTask = null;
            var backup = new BackupTask();
            SelectedBackupTask = backup;
        }

        public void SaveBackupTask(object parameter)
        {
            UpdateBindingGroup.CommitEdit();
            var backup = SelectedBackupTask as BackupTask;
            if (SelectedIndex == -1 || BackupTasks.Count == 0)
            {
                _backupTasksBusinessObject.AddBackupTask(backup);
                RaisePropertyChanged("BackupTasks");
            }
            else
            {
                _backupTasksBusinessObject.UpdateBackupTask(backup);
            }


            SelectedBackupTask = null;
        }

        #endregion

        #region  Private Methods

        private void BackupTaskChanged(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => { RaisePropertyChanged("BackupTasks"); }));
        }

        private void LogMessage(string message)
        {
            //var stringBuilder = new StringBuilder();
            //stringBuilder.Append(Logs);
            //stringBuilder.AppendLine();
            //stringBuilder.Append(message);
            Logs = message;
        }

        #endregion
    }
}