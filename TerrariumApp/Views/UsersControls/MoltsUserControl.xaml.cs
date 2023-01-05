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
using TerrariumApp.Views.Windows;

namespace TerrariumApp.Views.UsersControls
{
    /// <summary>
    /// Interaction logic for MoltsUserControl.xaml
    /// </summary>
    public partial class MoltsUserControl : UserControl
    {
        private CustomMessageBoxTranslation _translation = Globals.Translation.CustomMessageBoxTranslation;
        private bool _firstSelectionChangeInvokedByLoadingControl = true;

        public MoltsUserControl()
        {
            InitializeComponent();
            TranslateHeaders();
            MoltsViewModel viewModel = new();
            this.DataContext = viewModel;

            viewModel.ShowMessageBoXWhenDeleteMoltFailed = () =>
            {
                CustomMessageBox.ShowOK(_translation.ErrorCaption, _translation.MoltDeletingError, CustomMessageBoxImage.Error);
            };
        }

        private void TranslateHeaders()
        {
            MoltTranslation translation = Globals.Translation.MoltTranslation;
            moltDateHeader.Header = translation.MoltDate;
            descHeader.Header = translation.Description;
            hasImageHeader.Header = translation.HasImage;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSpiders.SelectedItem != null)
            {
                Globals.LastSelectedSpiderId = (cbSpiders.SelectedItem as Spider).SpiderId;
            }
            if (_firstSelectionChangeInvokedByLoadingControl == false)
            {
                btnComboBoxItemSelectionChangeHelper.Command.Execute(null);
            }    
            else
            {
                _firstSelectionChangeInvokedByLoadingControl = false;
            }
        }

        private void dgMolts_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                e.Handled = true;
                if (dgMolts.SelectedItem != null)
                {
                    Molt selectedMolt = dgMolts.SelectedItem as Molt;
                    DeleteMolt(selectedMolt);
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem selectedOption = sender as MenuItem;
            if (selectedOption.Name == "miAddMolt")
            {
                if (cbSpiders.SelectedItem != null && cbSpiders.SelectedItem.GetType() == typeof(Spider))
                {
                    Globals.LastSelectedSpiderId = (cbSpiders.SelectedItem as Spider).SpiderId;
                    VisualElementsHelper.GetMainMenuUserControl().OpenPage(MainMenuPages.AddMolt);
                }      
                else
                {
                    CustomMessageBox.ShowOK(_translation.WarningCaption, _translation.NoSelectedMoltWarning, CustomMessageBoxImage.Warning);
                }
            }
            else if (selectedOption.Name == "miDeleteMolt")
            {
                if (dgMolts.SelectedItem != null && dgMolts.SelectedItem.GetType() == typeof(Molt))
                {
                    Molt selectedMolt = dgMolts.SelectedItem as Molt;
                    DeleteMolt(selectedMolt);
                }                
            }
            else if (selectedOption.Name == "miShowMoltPhoto")
            {
                if (dgMolts.SelectedItem != null && dgMolts.SelectedItem.GetType() == typeof(Molt))
                {
                    Molt selectedMolt = dgMolts.SelectedItem as Molt;
                    ShowPhotoWindow showPhotoWindow = new(selectedMolt.ImagePath, this);
                    this.IsEnabled = false;
                    showPhotoWindow.ShowDialog();
                    this.IsEnabled = true;
                }
            }
        }

        private void DeleteMolt(Molt molt)
        {
            CustomMessageBoxResult result = CustomMessageBox.ShowYesNo(_translation.WarningCaption, _translation.ConfirmSelectedMoltToDelete + molt.MoltDate + " ?", CustomMessageBoxImage.Warning);
            if (result == CustomMessageBoxResult.YesOk)
            {
                btnDeleteMoltHelper.Command.Execute(molt);
            }
        }

        private void dgMolts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle show picture option in context menu is enable

            DataGrid grid = sender as DataGrid;
            if (grid.SelectedItem != null && grid.SelectedItem.GetType() == typeof(Molt))
            {
                Molt selectedItem = grid.SelectedItem as Molt;
                bool hasImage = selectedItem.HasImage;
                miShowMoltPhoto.IsEnabled = hasImage;
            }
        }
    }
}
