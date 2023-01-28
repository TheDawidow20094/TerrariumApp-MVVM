using Common.Models.SpiderModels;
using Common.Translation;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TerrariumApp.Helpers;
using TerrariumApp.VievsModels;

namespace TerrariumApp.Views.UsersControls
{
    /// <summary>
    /// Interaction logic for AddReproductionUserControl.xaml
    /// </summary>
    public partial class AddReproductionUserControl : UserControl
    {
        private CustomMessageBoxTranslation _maxBoxTranslation = Globals.Translation.CustomMessageBoxTranslation;
        private bool _successfullAddReproduction = true;

        public AddReproductionUserControl()
        {
            InitializeComponent();
            AddReproductionViewModel viewModel = new();
            this.DataContext = viewModel;

            viewModel.ShowMscBoxWhenAddReproductionFailed = () =>
            {
                _successfullAddReproduction = false;
                CustomMessageBox.ShowOK(_maxBoxTranslation.ErrorCaption, _maxBoxTranslation.ReproductionAddingError, CustomMessageBoxImage.Error);                
            };
        }

        private void btnAddReproduction_Click(object sender, RoutedEventArgs e)
        {
            if (e != null)
            {
                e.Handled = true;
            }
            if (CanAddReproduction())
            {
                btnAddReproduction.Command.Execute(GetReproductionToAdd());
                if (_successfullAddReproduction)
                {
                    VisualElementsHelper.GetMainMenuUserControl().OpenPage(MainMenuPages.Reproductions);
                }
                else
                {
                    _successfullAddReproduction = true; //reset
                }
            }
        }

        private bool CanAddReproduction()
        {
            bool canAdd = true;
            if (cbFemaleSPiders.SelectedItem == null || cbFemaleSPiders.SelectedItem.GetType() != typeof(Spider))
            {
                canAdd = false;
            }
            if (dpCopulationDate.SelectedDate == null)
            {
                canAdd = false;
                dpCopulationDate.BorderBrush = Globals.RedColor;
            }
            return canAdd;
        }

        private Reproduction GetReproductionToAdd()
        {
            string trimedDate = string.Empty;
            trimedDate = dpCopulationDate.SelectedDate.ToString().Substring(0, 10);
            Reproduction reproduction = new()
            {
                SpiderFemaleId = (cbFemaleSPiders.SelectedItem as Spider).SpiderId,
                CopulationDate = DateOnly.Parse(trimedDate),
                IsSuccessful = (bool)tbIsSuccessfull.IsEnabled,
                IsSpiderMaleEaten = (bool)tbIsSpiderMaleEaten.IsEnabled,
                IsCoccon = (bool)tbIsCoccon.IsEnabled,
                Note = tbxNote.Text
            };
            return reproduction;
        }

        private void dpCopulationDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpCopulationDate.SelectedDate != null)
            {
                dpCopulationDate.BorderBrush = null;
            }
            else
            {
                dpCopulationDate.BorderBrush = Globals.RedColor;
            }
        }
    }
}
