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
using TerrariumApp.Views.Base;

namespace TerrariumApp.VievsModels
{
    public class MoltsViewModel : BaseViewModel
    {
        #region TranslationRegion
        public string AddMoltContextMenuTranslation { get; set; }
        public string DeleteMoltContextMenuTranslation { get; set; }
        public string ShowImageContextMenuTranslation { get; set; }
        #endregion        
        private ISpider _ISpider = new SpiderServices(Globals.connParam);
        private IMolt _IMolt = new MoltServices(Globals.connParam);
        public ObservableCollection<Spider> SpidersList { get; set; } = new();
        public Spider SelectedSpider { get; set; }
        public ObservableCollection<Molt> MoltsList { get; set; } = new();
        public ICommand SpiderSelectionChangedCommand { get; set; }
        public ICommand DeleteMoltCommand { get; set; }
        public Action ShowMessageBoXWhenDeleteMoltFailed { get; set; }

        public MoltsViewModel()
        {
            Translate();
            FillSpiderList();
            FillMoltsList(null);
            SpiderSelectionChangedCommand = new RelayCommand(FillMoltsList);
            DeleteMoltCommand = new RelayCommand(DeleteMolt);
        }

        private void Translate()
        {
            ContextMenuTranslation translation = Globals.Translation.ContextMenuTranslation;
            AddMoltContextMenuTranslation = translation.AddMolt;
            DeleteMoltContextMenuTranslation = translation.DeleteMolt;
            ShowImageContextMenuTranslation = translation.ShowImage;
        }

        private void FillSpiderList()
        {
            SpidersList = _ISpider.GetUserSpiders(Globals.LocalUserData.UserId);
            if (Globals.LastSelectedSpiderId != -1)
            {
                SelectedSpider = SpidersList.FirstOrDefault(s => s.SpiderId == Globals.LastSelectedSpiderId);
            }
            else
            {
                SelectedSpider = SpidersList.FirstOrDefault();
            }            
        }

        public void FillMoltsList(object sender)
        {
            if (SelectedSpider != null)
            {
                ObservableCollection<Molt> moltsList = _IMolt.GetAllMolts(SelectedSpider.SpiderId);
                moltsList.Where(m => !string.IsNullOrEmpty(m.ImagePath)).ToList().ForEach(m => m.HasImage = true);
                MoltsList = moltsList;
            }            
        }

        public void DeleteMolt(object sender)
        {
            if (sender != null && sender.GetType() == typeof(Molt))
            {            
                Molt molt = sender as Molt;
                if (_IMolt.DeleteMolt(molt.MoltId, molt.SpiderId) == false)
                {
                    ShowMessageBoXWhenDeleteMoltFailed.Invoke();
                } 
                else
                {
                    MoltsList.Clear();
                    FillMoltsList(null);
                }
            }
        }
    }
}
