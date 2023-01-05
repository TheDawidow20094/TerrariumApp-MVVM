using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using TerrariumApp.Helpers;

namespace TerrariumApp.Views.Windows
{
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        private bool _canExit = false;

        public LoadingWindow()
        {
            InitializeComponent();
        }

        public void Start(string loadingText = "")
        {          
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.Width = this.Owner.ActualWidth / 2;
            this.Height = this.Owner.ActualHeight / 2;
            this.Owner.IsEnabled = false;
            if (string.IsNullOrEmpty(loadingText))
            {
                loadingText = Globals.Translation.LoadingText;
            }
            txtblLoadingText.Text = loadingText;
            this.Show();
        }

        public void Stop()
        {
            _canExit = true;
            this.Owner.IsEnabled = true;
            this.Close();
        }

        private void wLoadingWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_canExit)
            {
                e.Cancel = true;
            }
        }
    }
}
