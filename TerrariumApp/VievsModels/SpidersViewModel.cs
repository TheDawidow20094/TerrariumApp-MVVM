using Common.Models.SpiderModels;
using Common.Translation;
using Repository.Interfaces;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TerrariumApp.Helpers;
using TerrariumApp.Views.Base;

namespace TerrariumApp.VievsModels
{
    public class SpidersViewModel : BaseViewModel
    {
        #region TranslationRegion
        //public DataGridTextColumn NameTranslation { get; set; } = new();
        //public DataGridTextColumn TypeTranslation { get; set; } = new();
        //public DataGridTextColumn SpeciesTranslation { get; set; } = new(); 
        //public DataGridTextColumn BirthDateTranslation { get; set; } = new();
        //public DataGridTextColumn PurchaseDateTranslation { get; set; } = new();
        //public DataGridTextColumn LastFeedingDateTranslation { get; set; } = new();
        //public DataGridTextColumn IsActiveTranslation { get; set; } = new();
        //public DataGridTextColumn DeathDateTranslation { get; set; } = new();
        public string AddSpiderContextMenuTranslation { get; set; }
        public string DeleteSpiderContextMenuTranslation { get; set; }
        #endregion
        private ISpider _ISpider = new SpiderServices(Globals.connParam);
        public ObservableCollection<Spider> SpidersList { get; set; } = new();
        public Spider SelectedSpider { get; set; }
        public ICommand DeleteSelectedSpiderCommand { get; set; }
        public Action ShowMessageBoxDeletingSpiderFailed { get; set; }

        public SpidersViewModel()
        {
            Translate();
            GetAllUserSpiders();
            DeleteSelectedSpiderCommand = new RelayCommand(DeleteSelectedSpider);
        }

        private void Translate()
        {
            AddSpiderContextMenuTranslation = Globals.Translation.ContextMenuTranslation.AddSpider;
            DeleteSpiderContextMenuTranslation = Globals.Translation.ContextMenuTranslation.DeleteSpider;
            //SpiderTranslation translation = Globals.Translation.SpiderTranslation;
            //NameTranslation.Header = translation.Name;
            //TypeTranslation.Header = translation.Type;
            //SpeciesTranslation.Header = translation.Species;
            //BirthDateTranslation.Header = translation.BirthDate;
            //PurchaseDateTranslation.Header = translation.PurchaseDate;
            //LastFeedingDateTranslation.Header = translation.LastFeedingDate;
            //IsActiveTranslation.Header = translation.IsActive;
            //DeathDateTranslation.Header = translation.DeathDate;
        }

        private void GetAllUserSpiders()
        {
            SpidersList = _ISpider.GetUserSpiders(Globals.LocalUserData.UserId);
        }

        private void DeleteSelectedSpider(object sender)
        {
            if (SelectedSpider != null)
            {
                if (_ISpider.DeleteSpider(SelectedSpider.SpiderId, Globals.LocalUserData.UserId) == false)
                {
                    ShowMessageBoxDeletingSpiderFailed.Invoke();
                }
                else
                {
                    SpidersList.Remove(SpidersList.Select(s => s).Where(s => s.SpiderId == SelectedSpider.SpiderId).FirstOrDefault());
                }
            }            
        }
    }
}
