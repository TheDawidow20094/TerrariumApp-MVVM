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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TerrariumApp.Helpers;
using TerrariumApp.VievsModels;
using TerrariumApp.Views.UsersControls;

namespace TerrariumApp
{
    /// <summary>
    /// Interaction logic for AppFooterUserControl.xaml
    /// </summary>
    public partial class AppFooterUserControl : UserControl
    {
        public AppFooterUserControl()
        {
            InitializeComponent();
            this.DataContext = new AppFooterViewModel();
        }

        private void FooterBorders_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)sender;
            MainWindow mainWindow = VisualElementsHelper.GetMainWindow();
            switch (border.Name)
            {
                case "brdUserSettings":                    
                    UserSettingsUserControl userSettingsUserControl = new();
                    mainWindow.gridMainContent.Children.Clear();
                    mainWindow.gridMainContent.Children.Add(userSettingsUserControl);
                    break;
                case "brdSettings":
                    mainWindow.gridMainContent.Children.Clear();
                    SettingsUserControl settingsUserControl = new();
                    mainWindow.gridMainContent.Children.Add(settingsUserControl);
                    break;
            }
        }
    }
}
