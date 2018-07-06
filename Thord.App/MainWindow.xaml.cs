using System.Windows;

namespace Thord.App
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += (sender, args) => App.Current.Shutdown();
        }
    }
}
