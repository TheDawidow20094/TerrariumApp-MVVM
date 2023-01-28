using Common.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrariumApp.Views.Base;

namespace TerrariumApp.VievsModels
{
    public class MainMenuViewModel : BaseViewModel
    {
        #region TranslationRegion
        public string AddButtonLanguage { get; set; }
        public string AddSpiderLanguage { get; set; }
        public string AddMoltLanguage { get; set; }
        public string MainMenuButtonLanguage { get; set; }
        public string SpidersButtonLanguage { get; set; }
        public string MoltsButtonLanguage { get; set; }
        public string StatsButtonLanguage { get; set; }
        public string AddCopulationLanguage { get; set; }
        public string CopulationsButtonLanguage { get; set; }
        #endregion

        public MainMenuViewModel()
        {
            Translate();
        }

        private void Translate()
        {
            MainMenuTranslation translation = Globals.Translation.MainMenuTranslation;
            AddButtonLanguage = translation.AddButton;
            MainMenuButtonLanguage = translation.MainMenuButton;
            SpidersButtonLanguage = translation.SpidersButton;
            MoltsButtonLanguage = translation.MoltsButton;
            StatsButtonLanguage = translation.StatsButton;
            AddSpiderLanguage = translation.AddSpider;
            AddMoltLanguage = translation.AddMolt;
            AddCopulationLanguage = translation.AddCopulation;
            CopulationsButtonLanguage = translation.CopulationsButton;
        }
    }
}
