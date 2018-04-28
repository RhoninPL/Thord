using System;
using System.Windows;
using Ookii.Dialogs.Wpf;

namespace Thord.App
{
    public partial class CreatingTask : Window
    {
        public CreatingTask()
        {
            InitializeComponent();
            FirstRun();
            ChooseSourceFolderButton.Click += ChooseSourceFolder;
            ChooseTargetFolderButton.Click += ChooseTargetFolder;
        }

        private void ChooseSourceFolder(object sender, RoutedEventArgs routedEventArgs)
        {
            var dialog = new VistaFolderBrowserDialog();
            var selected = dialog.ShowDialog(Owner);

            if (selected == false)
                return;

            SourceDirectoryLabel.Content = dialog.SelectedPath;
            Properties.Settings.Default.SourceDefaultPath = dialog.SelectedPath;
            Properties.Settings.Default.Save();
        }

        private void ChooseTargetFolder(object sender, RoutedEventArgs routedEventArgs)
        {
            var dialog = new VistaFolderBrowserDialog();
            var selected = dialog.ShowDialog(Owner);

            if (selected == false)
                return;

            TargetDirectoryLabel.Content = dialog.SelectedPath;
            Properties.Settings.Default.TargetDefaultPath = dialog.SelectedPath;
            Properties.Settings.Default.Save();
        }

        private void FirstRun()
        {
            if (Properties.Settings.Default.SourceDefaultPath == Environment.GetFolderPath(Environment.SpecialFolder.UserProfile))
                Properties.Settings.Default.SourceDefaultPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (Properties.Settings.Default.TargetDefaultPath == Environment.GetFolderPath(Environment.SpecialFolder.UserProfile))
                Properties.Settings.Default.TargetDefaultPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            Properties.Settings.Default.Save();
        }
    }
}
