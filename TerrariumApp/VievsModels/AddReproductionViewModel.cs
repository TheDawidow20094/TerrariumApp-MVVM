using Common.Models.SpiderModels;
using Common.Translation;
using Repository.Interfaces;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TerrariumApp.Helpers;

namespace TerrariumApp.VievsModels
{
    public class AddReproductionViewModel
    {
        #region TranslationRegion
        public string FemaleSpiderOnlyTextTranslation { get; set; }
        public string CopulationDateTranslation { get; set; }
        public string IsSuccessfullTranslation { get; set; }
        public string IsSPiderMaleEatenTranslation { get; set; }
        public string IsCocconTranslation { get; set; }
        public string NoteTranslation { get; set; }
        public string AddCopulationButtonTranslation { get; set; }
        #endregion
        private IReproduction _iReproduction = new ReproductionService(Globals.connParam);
        private ISpider _iSpiders = new SpiderServices(Globals.connParam);
        public ObservableCollection<Spider> FemaleSpidersList { get; set; } = new();
        public Spider SelectedSpider { get; set; }
        public ICommand AddReproductionCommand { get; set; }
        public Action ShowMscBoxWhenAddReproductionFailed { get; set; }

        public AddReproductionViewModel()
        {
            Translate();
            FiltrSpiderListAndSelectSpider();
            AddReproductionCommand = new RelayCommand(AddReproduction);
        }
        
        private void Translate()
        {
            ReproductionTranslation translation = Globals.Translation.ReproductionTranslation;
            FemaleSpiderOnlyTextTranslation = translation.FemaleSpiderOnlyText;
            CopulationDateTranslation = translation.CopulationDate + " :";
            IsSuccessfullTranslation = translation.IsSuccessfull + " :";
            IsSPiderMaleEatenTranslation = translation.IsSPiderMaleEaten + " :";
            IsCocconTranslation = translation.IsCoccon + " :";
            NoteTranslation = translation.Note + " :";
            AddCopulationButtonTranslation = translation.AddCopulationButton;
        }

        /// <summary>
        /// Func select only female spiders, and select spider
        /// </summary>
        private void FiltrSpiderListAndSelectSpider()
        {
            ObservableCollection<Spider> spidersList = _iSpiders.GetUserSpiders(Globals.LocalUserData.UserId);
            FemaleSpidersList = new ObservableCollection<Spider>(spidersList.Where(s => s.Sex == "F"));
            SelectedSpider = FemaleSpidersList.FirstOrDefault();
        }

        public void AddReproduction(object sender)
        {
            if (sender != null && sender.GetType() == typeof(Reproduction))
            {
                if (_iReproduction.AddReproduction(sender as Reproduction) == false)
                {
                    ShowMscBoxWhenAddReproductionFailed.Invoke();
                }
            }
        }
    }
}
