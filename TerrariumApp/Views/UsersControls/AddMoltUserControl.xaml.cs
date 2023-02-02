using Common.Models.SpiderModels;
using Common.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for AddMoltUserControl.xaml
    /// </summary>
    public partial class AddMoltUserControl : UserControl
    {
        private CustomMessageBoxTranslation _translation = Globals.Translation.CustomMessageBoxTranslation;
        private string _imagePath = string.Empty;
        private bool _addMoltWasSuccessfull = true;

        public AddMoltUserControl()
        {
            InitializeComponent();
            AddMoltViewModel viewModel = new();
            this.DataContext = viewModel;

            viewModel.ShowMessageBoxWhenAddMoltFailed = () =>
            {
                _addMoltWasSuccessfull = false;
                CustomMessageBox.ShowOK(_translation.ErrorCaption, _translation.MoltAddingError, CustomMessageBoxImage.Error);
            };
        }

        private async void btnAddMolt_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CanAddMolt())
            {
                Globals.LastSelectedSpiderId = (cbSpiders.SelectedItem as Spider).SpiderId;
                Molt molt = new();
                molt.SpiderId = (cbSpiders.SelectedItem as Spider).SpiderId;
                string trimedDate = string.Empty;
                trimedDate = dpMoltDate.SelectedDate.ToString().Substring(0, 10);
                molt.MoltDate = DateOnly.Parse(trimedDate);
                molt.MoltDesc = tboxDescription.Text;
                molt.ImagePath = _imagePath;
                Button button = sender as Button;
                button.Command.Execute(molt);
                if (_addMoltWasSuccessfull)
                {
                    Globals.LastSelectedSpiderId = (cbSpiders.SelectedItem as Spider).SpiderId;
                    VisualElementsHelper.GetMainMenuUserControl().OpenPage(MainMenuPages.Molts);
                }
                else
                {
                    _addMoltWasSuccessfull = true; // reset value
                }
            }
        }
        
        private bool CanAddMolt()
        {
            bool result = true;
            if (cbSpiders.SelectedItem == null)
            {
                return false;
            }
            if (dpMoltDate.SelectedDate == null)
            {
                dpMoltDate.BorderBrush = Globals.RedColor;
                return false;
            }
            return result;
        }

        private void imageMolt_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {           
            UserImageHelper userImageHelper = new();
            userImageHelper.SaveImageAndGetImagePath(out _imagePath);
            if (!string.IsNullOrEmpty(_imagePath))
            {
                imageMolt.Source = new BitmapImage(new Uri(_imagePath));
            }
        }

        private void dpMoltDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpMoltDate.SelectedDate == null)
            {
                dpMoltDate.BorderBrush = Globals.RedColor;
            }
            else
            {
                dpMoltDate.BorderBrush = null;
            }
        }
    }
}
