using Common.Models;
using Common.Models.SpiderModels;
using Common.Translation;
using Repository.Interfaces;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TerrariumApp.Helpers;
using TerrariumApp.Views.Base;

namespace TerrariumApp.VievsModels
{
    public class UserSettingsViewModel : BaseViewModel
    {
        #region TranslationRegion
        public string AddButtonTranslation { get; set; }
        public string DeleteButtonTranslation { get; set; }
        public string UserName { get; set; }
        public string ConfirmAndExitButtonTranslation { get; set; }
        #endregion
        private IUser _IUser = new UserService(Globals.connParam);

        private ISpider _iSpider = new SpiderServices(Globals.connParam);
        public ObservableCollection<User> Users { get; set; } = new();
        public User SelectedUser { get; set; }
        public ICommand ChangeUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }
        public ICommand AddNewUserCommand { get; set; }
        public Action OpenMsxBoxWhenDeletingUserFailed { get; set; }
        public Action OpenMsxBoxWhenAddingUserFailed { get; set; }
        public Action OpenMsxBoxWhenUserHaveSpiders { get; set; }
        public ICommand DeletaAllUserSpiders { get; set; }
        public Action OpenMsxBoxWhenDeletingSpidersFailed { get; set; }
        private bool _openHomePage = true;


        public UserSettingsViewModel()
        {
            Translate();
            GetUsers();
            ChangeUserCommand = new RelayCommand(ChangeSelectedUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);
            AddNewUserCommand = new RelayCommand(AddNewUser);
            DeletaAllUserSpiders = new RelayCommand(DeleteAllUserSpiders);
        }  

        private void Translate()
        {
            UserSettingsTranslation translation = Globals.Translation.UserSettingsTranslation;
            AddButtonTranslation = translation.AddButton;
            DeleteButtonTranslation = translation.DeleteButton;
            UserName = translation.UserName;
            ConfirmAndExitButtonTranslation = translation.ConfirmAndExitButton;
        }
        
        /// <summary>
        /// Func gets users from database and shows in combobox
        /// </summary>
        /// <param name="setDefaultUser">Set first user in list as selected</param>
        private void GetUsers(bool setDefaultUser = true)
        {
            ObservableCollection<User> users = _IUser.GetLocalUsers();
            users.Select(u => u).ToList().ForEach(Users.Add);
            if (setDefaultUser)
            {
                SetFirstUserAsComboboxSelectedItem(users);
            }            
        }
        
        public void ChangeSelectedUser(object sender)
        {
            User selectedUser = sender as User;
            Globals.LastSelectedSpiderId = -1;
            Globals.LocalUserData = selectedUser;
            Globals.ApplicationConfig.LastLoggedUserId = selectedUser.UserId;
            Globals.ApplicationConfig.SerializeObject();
            if (_openHomePage)
            {
                VisualElementsHelper.GetMainMenuUserControl().OpenPage(MainMenuPages.HomePage);
            }    
            else
            {
                _openHomePage = true;
            }
        }

        private void SetFirstUserAsComboboxSelectedItem(ObservableCollection<User> users)
        {
            SelectedUser = users.Select(u => u).Where(u => u.UserId == Globals.ApplicationConfig.LastLoggedUserId).FirstOrDefault();
        }

        public void DeleteUser(object sender)
        {
            User userToDelete = sender as User;
            int userSpidersCount = _iSpider.GetUserSpidersCount(userToDelete.UserId);
            if (userSpidersCount > 0)
            {
                OpenMsxBoxWhenUserHaveSpiders.Invoke();
                return;
            }
            if(_IUser.DeleteUser(userToDelete.UserId) == false)
            {
                OpenMsxBoxWhenDeletingUserFailed.Invoke();
                return;
            }
            else
            {
                Users.Remove(userToDelete);
            }
            SelectedUser = Users.FirstOrDefault();
            _openHomePage = false;
            ChangeSelectedUser(SelectedUser);
        }

        public void DeleteAllUserSpiders(object sender)
        {
            User userToDelete = sender as User;
            if (userToDelete != null && userToDelete.GetType() == typeof(User))
            {
                ObservableCollection<Spider> spidersList = _iSpider.GetUserSpiders(SelectedUser.UserId);
                foreach (Spider spider in spidersList)
                {
                    if (_iSpider.DeleteSpider(spider.SpiderId, userToDelete.UserId) == false)
                    {
                        OpenMsxBoxWhenDeletingSpidersFailed.Invoke();
                        return;
                    }
                }
               DeleteUser(userToDelete);
            }                      
        }

        public void AddNewUser(object sender)
        {
            Tuple<string, string> userNameAndLanguage = sender as Tuple<string, string>;
            User newUser = _IUser.AddNewUser(userNameAndLanguage.Item1, userNameAndLanguage.Item2);
            if (newUser == null)
            {
                OpenMsxBoxWhenAddingUserFailed.Invoke();
            }
            else
            {
                Users.Clear();
                SelectedUser = newUser;
                GetUsers(false);
            }
        }
    }    
}
