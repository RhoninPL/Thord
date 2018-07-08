using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Ookii.Dialogs.Wpf;
using Thord.App.ViewModels;

namespace Thord.App
{
    public partial class CreatingTask : Window
    {
        public CreatingTask(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;

            //var drivers = Directory.GetLogicalDrives();

            //foreach (var driver in drivers)
            //{
            //    var item = new TreeViewItem
            //    {
            //        Header = driver,
            //        Tag = driver
            //    };

            //    item.Items.Add(null);
            //    item.Expanded += ItemOnExpanded;

            //    SourceFolderTree.Items.Add(item);
            //}

            //foreach (var driver in drivers)
            //{
            //    var item = new TreeViewItem
            //    {
            //        Header = driver,
            //        Tag = driver
            //    };

            //    item.Items.Add(null);
            //    item.Expanded += ItemOnExpanded;

            //    TargetFolderTree.Items.Add(item);
            //}

        }

        private void ItemOnExpanded(object sender, RoutedEventArgs routedEventArgs)
        {
            var item = (TreeViewItem)sender;

            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            item.Items.Clear();

            var folders = Directory.GetDirectories((string)item.Tag);

            foreach (var folder in folders)
            {
                try
                {
                    var subitem = new TreeViewItem
                    {
                        Header = Path.GetFileName(folder),
                        Tag = folder
                    };
                    subitem.Items.Add(null);
                    subitem.Expanded += ItemOnExpanded;

                    item.Items.Add(subitem);
                }
                catch
                {
                }
            }
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
