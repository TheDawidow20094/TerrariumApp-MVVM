using Repository.Controlers;
using Repository.Interfaces;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
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
using TerrariumApp.Views.Windows;

namespace TerrariumApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        public MainWindow()
        {
            IUser _iUser = new UserService(Globals.connParam);
            if (_iUser.GetLocalUsers() == null || _iUser.GetLocalUsers().Count == 0)
            {
                FirstRunConfiguratorWindow window = new();
                this.IsEnabled = false;
                window.ShowDialog();
                this.IsEnabled = true;
            }
            InitializeComponent();            
            RepositoryGlobals.logUserId = Globals.LocalUserData.UserId;
            RepositoryGlobals.logUserName = Globals.LocalUserData.UserName;
            VisualElementsHelper.GetMainMenuUserControl().OpenPage(MainMenuPages.HomePage);
        }

        private void wMianWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {            
            Globals.LoadingWindow.StopAndClose();            
            GC.SuppressFinalize(Globals.LoadingWindow);
            Globals.LoadingWindow = null;
            GC.Collect();
            Application.Current.Shutdown();
        }
    }
}
