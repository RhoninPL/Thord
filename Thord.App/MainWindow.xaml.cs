using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;

namespace Thord.App
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += (sender, args) => App.Current.Shutdown();
            AddNewTask.Click += AddNewTaskOnClick;
        }

        private void AddNewTaskOnClick(object o, RoutedEventArgs routedEventArgs)
        {
            var createTaskView = new CreatingTask();
            createTaskView.Show();
            // TODO: adding new tasks, saving tasks to app json config file, finish ui, progress bar?
        }
    }
}
