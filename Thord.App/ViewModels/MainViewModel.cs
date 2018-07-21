using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using Thord.App.Helpers;
using Thord.Core;
using Thord.Core.Configuration;
using Thord.Core.Models;
using static System.String;

namespace Thord.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<BackupTask> _backupTasks;
        private readonly BackupTasksBusinessObject _backupTasksBusinessObject;
        private object _selectedBackupTask;

        BindingGroup _UpdateBindingGroup;
        private string _logs;

        #endregion

        #region Properties

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

        public string Logs
        {
            get { return _logs; }
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
        }

        #endregion

        #endregion

        #region Public Methods

        public async void StartBackup(object parameter)
        {
            var synchronizeHanlder = new SynchronizeHandler(new WpfLogger(LogMessage));
            var backupTask = SelectedBackupTask as BackupTask;
            synchronizeHanlder.FoldersSkip = backupTask.FoldersToSkip;
            synchronizeHanlder.ShowErrors = backupTask.ShowErrors;

            var sourceFolder = new DirectoryInfo(backupTask.SourceDirectory);
            var targetFolder = new DirectoryInfo(backupTask.TargetDirectory);

            await synchronizeHanlder.StartCopy(sourceFolder, targetFolder);
        }

        public void OpenAddTaskWindow(object parameter)
        {
            new CreatingTask(this)
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