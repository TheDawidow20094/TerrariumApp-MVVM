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
    public class AddMoltViewModel : BaseViewModel
    {
        #region TranslationRegion
        public string DateTranslation { get; set; }
        public string DescriptionTranslation { get; set; }
        public string AddButtonTranslation { get; set; }
        #endregion
        private ISpider _ISpider = new SpiderServices(Globals.connParam);
        private IMolt _IMolt = new MoltServices(Globals.connParam);
        public ObservableCollection<Spider> Spiders { get; set; } = new();
        public Spider SelectedSpider { get; set; }
        public ICommand AddMoltCommand { get; set; }
        public Action ShowMessageBoxWhenAddMoltFailed { get; set; }

        public AddMoltViewModel()
        {
            Translate();
            FillSpidersList();
            AddMoltCommand = new RelayCommand(AddMolt);
        }

        private void Translate()
        {
            MoltTranslation translation = Globals.Translation.MoltTranslation;
            DateTranslation = translation.Date;
            DescriptionTranslation = translation.Description;
            AddButtonTranslation = translation.AddButton;
        }

        private void FillSpidersList()
        {
            Spiders = _ISpider.GetUserSpiders(Globals.LocalUserData.UserId);
            if (Globals.LastSelectedSpiderId != -1)
            {
                SelectedSpider = Spiders.FirstOrDefault(s => s.SpiderId == Globals.LastSelectedSpiderId);
            }
            else
            {
                SelectedSpider = Spiders.FirstOrDefault();
            }            
        }

        public void AddMolt(object sender)
        {
            Molt molt = sender as Molt;
            if (molt != null && molt.MoltDate != null)
            {
                if (_IMolt.AddMolt(molt, SelectedSpider.SpiderId) == false)
                {
                    ShowMessageBoxWhenAddMoltFailed.Invoke();
                }
            }            
        }
    }
}
