using Common.Translation;
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

namespace TerrariumApp.Views.Windows
{
    /// <summary>
    /// Interaction logic for CustomMessageBoxWindow.xaml
    /// </summary>
    public partial class CustomMessageBoxWindow : Window
    {
        private CustomMessageBoxTranslation _translation = Globals.Translation.CustomMessageBoxTranslation;
        private CustomMessageBoxResult _customMessageBoxResult;

        public CustomMessageBoxWindow(string caption, string content, CustomMessageBoxImage messageBoxImage, bool showOnlyOneButton)
        {
            InitializeComponent();
            PrepareWindow(caption, content, messageBoxImage, showOnlyOneButton);
        }

        private void PrepareWindow(string caption, string content, CustomMessageBoxImage messageBoxImage, bool showOnlyOneButton)
        {                        
            SetImage(messageBoxImage);
            txtblCaption.Text = caption;
            txtblContent.Text = content;
            if (!showOnlyOneButton)
            {
                btnOkYes.Content = _translation.YesButton;
                btnOkYes.Margin = new Thickness(0,0,30,0);
                btnNo.Visibility = Visibility.Visible;
                btnNo.Content = _translation.NoButton;
            }
            btnOkYes.Focus();
        }

        private void SetImage(CustomMessageBoxImage messageBoxImage)
        {
            switch (messageBoxImage)
            {
                case CustomMessageBoxImage.Warning:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/warning.png"));
                    break;
                case CustomMessageBoxImage.Error:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/error.png"));
                    break;
            }
        }

        private void wCustomMessageBox_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_customMessageBoxResult == null)
            {
                this.Tag = CustomMessageBoxResult.Exit;
            }
            else
            {
                this.Tag = _customMessageBoxResult;
            }
        }

        private void ButtonsClick_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Name)
            {
                case "btnOkYes":
                    _customMessageBoxResult = CustomMessageBoxResult.YesOk;
                    this.Close();
                    break;
                case "btnNo":
                    _customMessageBoxResult = CustomMessageBoxResult.No;
                    this.Close();
                    break;
            }
        }
    }
}
