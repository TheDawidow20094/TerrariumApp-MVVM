using Common.Models.SpiderModels;
using Common.Translation;
using Repository.Interfaces;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TerrariumApp.Helpers;
using TerrariumApp.Views.Base;

namespace TerrariumApp.VievsModels
{
    public class ReproductionsViewModel : BaseViewModel
    {
        #region TranslationRegion
        public string MenuItemAddCopulationTranslation { get; set; }
        public string MenuItemDeleteCopulationTranslation { get; set; }
        public string MenuItemShowNoteTranslation { get; set; }
        #endregion

        private ISpider _iSpider = new SpiderServices(Globals.connParam);
        private IReproduction _iReproduction = new ReproductionService(Globals.connParam);

        public Spider SelectedFemaleSpider { get; set; }
        public ObservableCollection<Spider> FemaleSpidersList { get; set; }
        public ObservableCollection<Reproduction> ReproductionsList { get; set; } = new();
        public ICommand FemaleSpiderSelectionChange { get; set; }
        public ICommand DeleteReproductionCommand { get; set; }
        public Action ShowMessageBoxDeletingReproductionFailed { get; set; }

        public ReproductionsViewModel()
        {
            Translate();
            FillSpiderList();
            FillReprodusctionsList(null);
            FemaleSpiderSelectionChange = new RelayCommand(FillReprodusctionsList);
            DeleteReproductionCommand = new RelayCommand(DeleteReproduction);
        }

        private void Translate()
        {
            ContextMenuTranslation translation = Globals.Translation.ContextMenuTranslation;
            MenuItemAddCopulationTranslation = translation.AddCopulation;
            MenuItemDeleteCopulationTranslation = translation.DeleteCopulation;
            MenuItemShowNoteTranslation = translation.ShowNote;
        }

        private void FillSpiderList()
        {
            FemaleSpidersList = new ObservableCollection<Spider>(_iSpider.GetUserSpiders(Globals.LocalUserData.UserId).Where(s => s.Sex == "F"));
            SelectedFemaleSpider = FemaleSpidersList.FirstOrDefault();
        }

        public void FillReprodusctionsList(object sender)
        {
            if (SelectedFemaleSpider != null)
            {
                ReproductionsList = _iReproduction.GetSpiderReproductions(SelectedFemaleSpider.SpiderId);                
            }
        }

        public void DeleteReproduction(object sender)
        {
            if (SelectedFemaleSpider != null && sender != null)
            {
                if (_iReproduction.DeleteReproduction(SelectedFemaleSpider.SpiderId, sender.ToString()) == false)
                {
                    ShowMessageBoxDeletingReproductionFailed.Invoke();
                }
                else
                {
                    ReproductionsList.Clear();
                    FillReprodusctionsList(null);
                }
            }
        }
    }
}
