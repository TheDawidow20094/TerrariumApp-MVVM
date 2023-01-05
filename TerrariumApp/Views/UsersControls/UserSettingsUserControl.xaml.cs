using Common.Models;
using Common.Translation;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TerrariumApp.Helpers;
using TerrariumApp.VievsModels;

namespace TerrariumApp.Views.UsersControls
{
    /// <summary>
    /// Interaction logic for UserSettingsUserControl.xaml
    /// </summary>
    public partial class UserSettingsUserControl : UserControl
    {
        private bool _addingUserWasSuccesfull = true;
        private CustomMessageBoxTranslation _translation = Globals.Translation.CustomMessageBoxTranslation;

        public UserSettingsUserControl()
        {
            InitializeComponent();
            this.DataContext = new UserSettingsViewModel();
            UserSettingsViewModel viewModel = (UserSettingsViewModel)DataContext;

            viewModel.OpenMsxBoxWhenDeletingUserFailed = () =>
            {
                CustomMessageBox.ShowOK(_translation.ErrorCaption, _translation.UserDeletingError, CustomMessageBoxImage.Error);
            };
            viewModel.OpenMsxBoxWhenAddingUserFailed = () =>
            {
                _addingUserWasSuccesfull = false;
                CustomMessageBox.ShowOK(_translation.ErrorCaption, _translation.UserAddingError, CustomMessageBoxImage.Error);
            };
            viewModel.OpenMsxBoxWhenUserHaveSpiders = () =>
            {
                CustomMessageBoxResult result = CustomMessageBox.ShowYesNo(_translation.WarningCaption, _translation.ConfirmDeletingAllUSerSpiders, CustomMessageBoxImage.Warning);
                if (result == CustomMessageBoxResult.YesOk)
                {
                    btnDeleteAllUserSpidersHelper.Command.Execute(cboxUsers.SelectedItem);
                }
            };
            viewModel.OpenMsxBoxWhenDeletingSpidersFailed = () =>
            {
                CustomMessageBox.ShowOK(_translation.ErrorCaption, _translation.DeletingAllSpidersError + (cboxUsers.SelectedItem as User).UserName, CustomMessageBoxImage.Error);;
            };
        }

        private void btnDeleteUser_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (cboxUsers.Items.Count == 1)
            {
                CustomMessageBox.ShowOK(_translation.WarningCaption, _translation.DeletingAllUsersForbidden, CustomMessageBoxImage.Warning);
                return;
            }
            Button button = sender as Button;
            User selectedUser = cboxUsers.SelectedItem as User;
            CustomMessageBoxResult result = CustomMessageBox.ShowYesNo(_translation.WarningCaption, _translation.ConfirmSelectedUserToDelete + selectedUser.UserName + " ?",
                CustomMessageBoxImage.Warning);;
            if (result == CustomMessageBoxResult.YesOk)
            {
                button.Command.Execute(button.CommandParameter);
            }            
        }

        private void btnAddNewUser_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e != null)
            {
                e.Handled = true;
            }            
            if (borderAddNewUser.IsVisible)
            {
                if (string.IsNullOrEmpty(tbxUserName.Text))
                {
                    return;
                }
                Button button = sender as Button;
                Tuple<string, string> userNameAndLanguage = new Tuple<string, string>(tbxUserName.Text, Globals.Translation.LanguageSymbol);
                button.Command.Execute(userNameAndLanguage);
                borderAddNewUser.Visibility = Visibility.Collapsed;
                tbxUserName.Text = string.Empty;
                if (_addingUserWasSuccesfull)
                {
                    UserSettingsViewModel viewModel = (this.DataContext as UserSettingsViewModel);
                    Globals.LocalUserData = viewModel.SelectedUser;
                    cboxUsers.SelectedItem = cboxUsers.Items[cboxUsers.Items.Count - 1];
                    btnConfirm.Command.Execute(Globals.LocalUserData);
                }
                else
                {
                    _addingUserWasSuccesfull = true;
                }
            }
            else
            {
                borderAddNewUser.Visibility = Visibility.Visible;
                tbxUserName.Focus();
            }
        }

        private void tbxUserName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                btnAddNewUser_PreviewMouseDown(btnAddNewUser, null);
            }
        }

        private void btnConfirm_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (borderAddNewUser.IsVisible)
            {
                if (!string.IsNullOrWhiteSpace(tbxUserName.Text))
                {
                    btnAddNewUser_PreviewMouseDown(btnAddNewUser, null);                    
                }
                return;
            }
        }
    }
}
