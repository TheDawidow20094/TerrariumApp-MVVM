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
using System.Windows.Input;
using TerrariumApp.Helpers;
using TerrariumApp.Views.Base;

namespace TerrariumApp.VievsModels
{
    public class AddSpiderViewModel : BaseViewModel
    {
        #region TranslationRegion
        public string NameTranslation { get; set; }
        public string TypeTranslation { get; set; }
        public string SpeciesTranslation { get; set; }
        public string BirthDateTranslation { get; set; }
        public string PurchaseDateTranslation { get; set; }
        public string LastFeedingDateTranslation { get; set; }
        public string IsActiveTranslation { get; set; }
        public string AddSpiderButtonTranslation { get; set; }
        public string ReturnButtonTranslation { get; set; }
        public string SexTranslation { get; set; }
        public string AddManyTranslation { get; set; }
        #endregion
        private ISpecies _ISpecies = new SpeciesServices(Globals.connParam);
        private ISpider _ISpider = new SpiderServices(Globals.connParam);
        public ICommand AddSpiderCommand { get; set; }
        public Action ShowMessageBoxSpiderAddFailed { get; set; }
        public ObservableCollection<string> SpeciesList { get; set; } = new();
        public string SelectedSpecies { get; set; }

        public AddSpiderViewModel()
        {
            Translate();
            AddSpecies();
            AddSpiderCommand = new RelayCommand(AddSpider);
        }

        private void Translate()
        {
            SpiderTranslation translation = Globals.Translation.SpiderTranslation;
            NameTranslation = translation.Name + " :";
            TypeTranslation = translation.Type + " :";
            SpeciesTranslation = translation.Species + " :";
            BirthDateTranslation = translation.BirthDate + " :";
            PurchaseDateTranslation = translation.PurchaseDate + " :";
            LastFeedingDateTranslation = translation.LastFeedingDate + " :";
            IsActiveTranslation = translation.IsActive + " :";
            AddSpiderButtonTranslation = translation.AddSpiderButton;
            ReturnButtonTranslation = translation.ReturnButton;
            SexTranslation = translation.Sex + " :";
            AddManyTranslation = translation.AddManySpiders;
        }

        private void AddSpecies()
        {
            SpeciesList = _ISpecies.GetSpecies(Globals.ApplicationConfig.AppLanguage);
            SelectedSpecies = SpeciesList.FirstOrDefault();
        }

        public void AddSpider(object sender)
        {
            Spider spider = sender as Spider;
            if (_ISpider.AddSpider(spider, Globals.LocalUserData.UserId))
            {
            }
            else
            {
                ShowMessageBoxSpiderAddFailed.Invoke();
            }
        }
    }
}
