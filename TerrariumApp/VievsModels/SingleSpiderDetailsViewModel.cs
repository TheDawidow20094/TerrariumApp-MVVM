using Common.Models.SpiderModels;
using Common.Translation;
using Prism.Commands;
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

namespace TerrariumApp.VievsModels
{
    public class SingleSpiderDetailsViewModel
    {
        #region TranslationRegion
        public string NameTranslation { get; set; }
        public string TypeTranslation { get; set; }
        public string SpeciesTranslation { get; set; }
        public string BirthDateTranslation { get; set; }
        public string PurchaseDateTranslation { get; set; }
        public string LastFeedingDateTranslation { get; set; }
        public string IsActiveTranslation { get; set; }
        public string DeathDateTranslation { get; set; }
        public string SaveButtonTranslation { get; set; }
        public string SexTranslation { get; set; }
        #endregion
        private ISpecies _ISpecies = new SpeciesServices(Globals.connParam);
        private ISpider _ISpider = new SpiderServices(Globals.connParam);
        public int UserId { get; set; }
        public int SpiderId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public ComboBoxItem SelectedSex { get; set; }
        public List<ComboBoxItem> Sex { get; set; } = new();
        public ObservableCollection<string> SpeciesList { get; set; } = new();
        public string Species { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? LastFeedingDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public bool IsActive { get; set; }
        public string ImagePath { get; set; }
        public ICommand EditSpiderCommand { get; set; }
        public Action NonSuccessfullEditSPider { get; set; }

        public SingleSpiderDetailsViewModel(Spider spider)
        {
            Translate();
            GetSpecies();
            UserId = spider.UserId;
            SpiderId = spider.SpiderId;
            Name = spider.Name;
            Type = spider.Type;
            List<string> sexValues = new List<string> { "M", "F" };
            Sex.AddRange(sexValues.Select(x => new ComboBoxItem { Content = x }));
            SelectedSex = Sex.FirstOrDefault(i => i.Content.ToString() == spider.Sex.ToString());            
            Species = spider.Species;
            BirthDate = spider.BirthDate != null ? Convert.ToDateTime(spider.BirthDate.ToString()) : null;
            PurchaseDate = spider.PurchaseDate != null ? Convert.ToDateTime(spider.PurchaseDate.ToString()) : null;
            LastFeedingDate = spider.LastFeedingDate != null ? Convert.ToDateTime(spider.LastFeedingDate.ToString()) : null;
            DeathDate = spider.DeathDate != null ? Convert.ToDateTime(spider.DeathDate.ToString()) : null;
            IsActive = spider.IsActive;
            ImagePath = spider.ImagePath;
            EditSpiderCommand = new RelayCommand(EditSpider);
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
            DeathDateTranslation = translation.DeathDate + " :";
            SaveButtonTranslation = translation.SaveChangesAndExitButton;
            SexTranslation = translation.Sex + " :";
        }

        private void GetSpecies()
        {
            SpeciesList = _ISpecies.GetSpecies(Globals.ApplicationConfig.AppLanguage);
        }

        
        public void EditSpider(object sender)
        {
            if (sender != null && sender.GetType() == typeof(Spider))
            {
                Spider spider = (Spider)sender;
                if (_ISpider.UpdateSpider(spider, Globals.LocalUserData.UserId) == false)
                {
                    NonSuccessfullEditSPider.Invoke();
                }
            }                      
        }
    }
}
