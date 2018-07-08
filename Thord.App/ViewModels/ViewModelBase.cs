using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace Thord.App.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Fields

        bool? _closeWindowFlag;

        #endregion

        #region Properties

        public bool? CloseWindowFlag
        {
            get
            {
                return _closeWindowFlag;
            }
            set
            {
                _closeWindowFlag = value;
                RaisePropertyChanged("CloseWindowFlag");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Methods

        public virtual void CloseWindow(bool? result = true)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                CloseWindowFlag = CloseWindowFlag == null
                    ? true
                    : !CloseWindowFlag;
            }));
        }

        internal void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }
}