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
using System.Windows;
using System.Windows.Input;
using TerrariumApp.Helpers;
using TerrariumApp.Views.Base;

namespace TerrariumApp.VievsModels
{
    public class HomePageViewModel : BaseViewModel
    {
        #region TranslationRegion
        public string WelcomeTranslation { get; set; }
        public string YourSpidersTranslation { get; set; }
        public string QickViewTranslation { get; set; }
        public string SexTranslation { get; set; }
        public string LastFeedingDateTranslation { get; set; }
        public string MoltsTranslation { get; set; }
        public string IsActiveTranslation { get; set; }
        public string SpidersCountTranslation { get; set; }
        public string NoSpiderInformationTextTranslation { get; set; }
        #endregion
        private IMolt _iMolt = new MoltServices(Globals.connParam);
        private ISpider _iSpider = new SpiderServices(Globals.connParam);
        public ObservableCollection<Spider> SpidersList { get; set; } = new();
        public Spider SelectedSpider { get; set; }
        public string LastFeedingDate { get; set; }
        public string Molts { get; set; }
        public string IsActiveText { get; set; }
        public string SpidersCount { get; set; }
        public string Sex { get; set; }
        public Visibility QuickDataViewVisibility { get; set; } = Visibility.Collapsed;
        public Visibility NoSpiderInfoVisibility { get; set; } = Visibility.Collapsed;
        public ICommand SpiderChangeCommand { get; set; }
        public string UserName { get; set; } = Globals.LocalUserData.UserName;

        public HomePageViewModel()
        {
            Translate();
            FillSpidersList();
            FillQuickViewData();
            SpiderChangeCommand = new RelayCommand(ReloadQuickView);
        }

        private void Translate()
        {
            HomePageTranslation translation = Globals.Translation.HomePageTranslation;
            WelcomeTranslation = translation.Welcome;
            YourSpidersTranslation = translation.YourSpiders;
            QickViewTranslation = translation.QickView;
            SexTranslation = translation.Sex;
            LastFeedingDateTranslation = translation.LastFeedingDate;
            MoltsTranslation = translation.Molts;
            IsActiveTranslation = translation.IsActive;
            SpidersCountTranslation = translation.SpidersCount;
            NoSpiderInformationTextTranslation = translation.NoSpiderInfoText;
        }

        private void FillSpidersList()
        {
            SpidersList = _iSpider.GetUserSpiders(Globals.LocalUserData.UserId);
            if (Globals.LastSelectedSpiderId != -1)
            {
                SelectedSpider = SpidersList.FirstOrDefault(s => s.SpiderId == Globals.LastSelectedSpiderId);
            }
            else
            {
                SelectedSpider = SpidersList.FirstOrDefault();
            }
        }

        private void FillQuickViewData()
        {
            if (SpidersList.Count > 0)
            {
                NoSpiderInfoVisibility = Visibility.Collapsed;
                QuickDataViewVisibility = Visibility.Visible;
                Sex = SelectedSpider.Sex;
                LastFeedingDate = string.IsNullOrEmpty(SelectedSpider.LastFeedingDate.ToString()) ? "----" : SelectedSpider.LastFeedingDate.ToString();
                Molts = _iMolt.GetSpiderMoltsCount(SelectedSpider.SpiderId, Globals.LocalUserData.UserId).ToString();
                IsActiveText = SelectedSpider.IsActive ? Globals.Translation.HomePageTranslation.Yes : Globals.Translation.HomePageTranslation.No;
                SpidersCount = _iSpider.GetUserSpidersCount(Globals.LocalUserData.UserId).ToString();
            }
            else
            {
                NoSpiderInfoVisibility = Visibility.Visible;
                QuickDataViewVisibility = Visibility.Collapsed;
            }
        }

        public void ReloadQuickView(object sender)
        {
            FillQuickViewData();
        }
    }
}
