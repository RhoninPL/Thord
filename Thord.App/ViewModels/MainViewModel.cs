using System;
using System.Collections.ObjectModel;
using System.Windows;
using Thord.Core;
using Thord.Core.Models;

namespace Thord.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            BackupTasks = new ObservableCollection<BackupTask>
            {
                new BackupTask {SourceDirectory = "C:\\", TargetDirectory = "D:\\"},
                new BackupTask {SourceDirectory = "E:\\", TargetDirectory = "D:\\"},
                new BackupTask {SourceDirectory = "F:\\", TargetDirectory = "D:\\"}
            };
            OpenAddTaskCommand = new RelayCommand(OpenAddTaskWindow);
            StartBackupCommand = new RelayCommand(StartBackup);
        }

        public ObservableCollection<BackupTask> BackupTasks { get; set; }

        public RelayCommand OpenAddTaskCommand { get; set; }
        public RelayCommand StartBackupCommand { get; set; }
        public BackupTask SelectedBackupTask { get; set; }

        public void StartBackup(object parameter)
        {
            MessageBox.Show("Test");
        }

        public void OpenAddTaskWindow(object parameter)
        {
            new CreatingTask(this).Show();
        }
    }
}