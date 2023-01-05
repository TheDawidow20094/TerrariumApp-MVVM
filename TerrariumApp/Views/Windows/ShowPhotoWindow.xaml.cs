using Common.Translation;
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
using System.Windows.Shapes;
using TerrariumApp.Helpers;

namespace TerrariumApp.Views.Windows
{
    /// <summary>
    /// Interaction logic for ShowPhotoWindow.xaml
    /// </summary>
    public partial class ShowPhotoWindow : Window
    {
        private CustomMessageBoxTranslation _translation = Globals.Translation.CustomMessageBoxTranslation;
        private string _imagePath;
        private UserControl _owner;
        private string _windowTitle;

        public ShowPhotoWindow(string imagePath, UserControl owner, string windowTitle = "")
        {
            InitializeComponent();    
            _imagePath = imagePath;
            _owner = owner;
            _windowTitle = windowTitle;
            this.Owner = VisualElementsHelper.GetMainWindow();
            this.Width = _owner.ActualWidth;
            this.Height = _owner.ActualHeight;
            //this.Height = this.Owner.ActualHeight / 2;
            //this.Width = this.Owner.ActualWidth / 2;
        }

        private void wShowPhoto_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void wShowPhoto_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingWindow loading = new();
            loading.Owner = this;
            loading.Start();            
            this.Title = _windowTitle;
            if (!File.Exists(_imagePath))
            {
                loading.Stop();
                CustomMessageBox.ShowOK(_translation.ErrorCaption, _translation.PhotoNoFound, CustomMessageBoxImage.Error);
                this.Close();
            }
            else
            {
                image.Source = new BitmapImage(new Uri(_imagePath));
                loading.Stop();
            }
        }
    }
}
