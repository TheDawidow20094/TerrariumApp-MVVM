using Common.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrariumApp.Views.Base;

namespace TerrariumApp.VievsModels
{
    public class AppFooterViewModel : BaseViewModel
    {
        #region TranslationRegion
        public string UserSettingsTranslation { get; set; }
        public string SettingsTranslation { get; set; }
        #endregion
        public string LoggedUserName { get; set; }
        public string AppVersion { get; set; }

        public AppFooterViewModel()
        {
            Translate();
            LoggedUserName = Globals.LocalUserData.UserName;
            AppVersion = Globals.ApplicationConfig.AppVersion;
        }

        private void Translate()
        {
            MainMenuTranslation translation = Globals.Translation.MainMenuTranslation;
            UserSettingsTranslation = translation.UserSettingsFooter;
            SettingsTranslation = translation.SettingsFooter;
        }
    }
}
