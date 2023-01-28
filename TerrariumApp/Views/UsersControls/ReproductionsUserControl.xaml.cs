using Common.Models.SpiderModels;
using Common.Translation;
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
    /// Interaction logic for ReproductionsUserControl.xaml
    /// </summary>
    public partial class ReproductionsUserControl : UserControl
    {
        private bool _firstSelectionChangeInvokedByLoadingControl = true;
        private CustomMessageBoxTranslation _translation = Globals.Translation.CustomMessageBoxTranslation;

        public ReproductionsUserControl()
        {
            InitializeComponent();
            TranslateHeaders();
            ReproductionsViewModel viewModel = new ReproductionsViewModel();
            this.DataContext = viewModel;

            viewModel.ShowMessageBoxDeletingReproductionFailed = () =>
            {
                CustomMessageBox.ShowOK(_translation.ErrorCaption, _translation.ReproductionRemoveError, CustomMessageBoxImage.Error);
            };
        }

        private void cbFemaleSpiders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFemaleSpiders.SelectedItem != null)
            {
                Globals.LastSelectedSpiderId = (cbFemaleSpiders.SelectedItem as Spider).SpiderId;
            }
            if (_firstSelectionChangeInvokedByLoadingControl == false)
            {
                btSelectionChangedHelper.Command.Execute(null);
            }
            else
            {
                _firstSelectionChangeInvokedByLoadingControl = false;
            }
        }

        private void TranslateHeaders()
        {
            ReproductionTranslation translation = Globals.Translation.ReproductionTranslation;
            copulationDateHeader.Header = translation.CopulationDate;
            isSuccessfulHeader.Header = translation.IsSuccessfull;
            isSpiderMaleEatenHeader.Header = translation.IsSPiderMaleEaten;
            isCocconHeader.Header = translation.IsCoccon;
            noteHeader.Header = translation.Note;
            tblFemaleSpiderOnly.Text = translation.FemaleSpiderOnlyText;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem.Name == "miAddReproduction")
            {
                VisualElementsHelper.GetMainMenuUserControl().OpenPage(MainMenuPages.AddReproduction);
            }
            else if (menuItem.Name == "miDeleteReproduction")
            {
                if (dgReproductions.SelectedItem != null && dgReproductions.SelectedItem.GetType() == typeof(Reproduction))
                {
                    Reproduction selectedReproduction = dgReproductions.SelectedItem as Reproduction;
                    DeleteReproduction(selectedReproduction);
                }
            }
            else if (menuItem.Name == "miShowNote")
            {
                if (dgReproductions.SelectedItem != null && dgReproductions.SelectedItem.GetType() == typeof(Reproduction))
                {
                    Reproduction selectedReproduction = dgReproductions.SelectedItem as Reproduction;
                    CustomMessageBox.ShowOK(_translation.NoteCaption, selectedReproduction.Note, CustomMessageBoxImage.None);
                }                    
            }
        }

        private void gcReproductions_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                e.Handled = true;
                if (dgReproductions.SelectedItem != null && dgReproductions.SelectedItem.GetType() == typeof(Reproduction))
                {
                    Reproduction selectedReproduction = dgReproductions.SelectedItem as Reproduction;
                    DeleteReproduction(selectedReproduction);
                }
            }
        }

        private void DeleteReproduction(Reproduction reproduction)
        {
            CustomMessageBoxResult result = CustomMessageBox.ShowYesNo(_translation.WarningCaption, _translation.ConfirmSelectedReproductionToDelete + reproduction.CopulationDate + " ?", CustomMessageBoxImage.Warning);
            if (result == CustomMessageBoxResult.YesOk)
            {
                btnDeleteReproductionHelper.Command.Execute(reproduction.CopulationDate);
            }
        }        
    }
}
