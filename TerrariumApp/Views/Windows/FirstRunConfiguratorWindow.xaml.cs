using Common;
using Common.Models;
using Repository.Interfaces;
using Repository.Services;
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
    /// Interaction logic for FirstRunConfiguratorWindow.xaml
    /// </summary>
    public partial class FirstRunConfiguratorWindow : Window
    {
        private IUser _iUser = new UserService(Globals.connParam);
        private string _selectedLanguage;
        private bool _canClose = false;
        public FirstRunConfiguratorWindow()
        {
            InitializeComponent();
        }

        private void LanguageButtons_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {          
            Button button = sender as Button;            
            switch (button.Name)
            {
                case "btnPolishLanguage":
                    _selectedLanguage = "PL";
                    tblTypeUsernameText.Text = "Wpisz nazwę nazwę użytkownika :";
                    btnConfirm.Content = "Zatwierdź";
                    break;
                case "btnEnglishLanguage":
                    _selectedLanguage = "EN";
                    tblTypeUsernameText.Text = "Enter username :";
                    btnConfirm.Content = "Confirm";
                    break;
            }
            spLanguage.Visibility = Visibility.Collapsed;
            spUsername.Visibility = Visibility.Visible;
            btnConfirm.Visibility = Visibility.Visible;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbxUsername.Text))
            {
                User user = _iUser.AddNewUser(tbxUsername.Text, _selectedLanguage);
                if (user != null)
                {
                    Globals.LocalUserData = user;
                    Globals.ApplicationConfig.AppLanguage = _selectedLanguage;
                    Globals.ApplicationConfig.LastLoggedUserId = user.UserId;
                    Globals.ApplicationConfig.SerializeObject();
                    Globals.SetNewLanguage(_selectedLanguage, false);
                    Globals.Log.WriteLog(this.GetType().Name, "First run creadted user!", LogType.ImportantMessage, user.UserId, user.UserName);
                    _canClose = true;
                    this.Close();
                }
                else
                {
                    tbxUsername.Background = Globals.RedColor;
                    return;
                }
            }
            else
            {
                tbxUsername.Background = Globals.RedColor;
                return;
            }
        }

        private void tbxUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxUsername.Text))
            {
                tbxUsername.Background = Globals.RedColor;
            }
            else
            {
                tbxUsername.Background = Globals.ControlsColor;
            }
        }

        private void wFirstRunConfigurator_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_canClose == false)
            {
                e.Cancel = true;
            }
        }
    }
}
