using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Thord.App.ViewModels
{
    public class TaskViewModel : ViewModelBase
    {
        #region Fields

        private TreeViewItem _folders;

        #endregion

        #region Properties

        public IEnumerable<string> Folders
        {
            get
            {
                //return _folders;
                return new List<string>
                {
                    "test1",
                    "test2"
                };
            }
            //set
            //{
            //    _folders = value;
            //    RaisePropertyChanged("Folders");
            //}
        }

        #endregion

        #region Constructors

        public TaskViewModel()
        {
            //var drivers = Directory.GetLogicalDrives();
            //Folders = new TreeViewItem();

            //foreach (var driver in drivers)
            //{
            //    var item = new TreeViewItem
            //    {
            //        Header = driver,
            //        Tag = driver
            //    };

            //    item.Items.Add(null);
            //    item.Expanded += ItemOnExpanded;

            //    Folders.Items.Add(item);
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
            //}
        }

        #endregion

        #region  Private Methods

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

        #endregion
    }
}