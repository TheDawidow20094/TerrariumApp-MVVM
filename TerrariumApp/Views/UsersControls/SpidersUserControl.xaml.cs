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
    /// Interaction logic for SpidersUserControl.xaml
    /// </summary>
    public partial class SpidersUserControl : UserControl
    {
        private CustomMessageBoxTranslation _translation = Globals.Translation.CustomMessageBoxTranslation;
        public SpidersUserControl()
        {
            InitializeComponent();
            Globals.IsSpiderAlternativeView = false;
            TranslateHeaders();
            SpidersViewModel viewModel = new();
            this.DataContext = viewModel;

            viewModel.ShowMessageBoxDeletingSpiderFailed = () =>
            {
                CustomMessageBox.ShowOK(_translation.ErrorCaption, _translation.SpiderDeletingError, CustomMessageBoxImage.Error);
            };
        }

        private void dgSpiders_PreviewKeyDown(object sender, KeyEventArgs e)
        {            
            if (e.Key == Key.Delete)
            {
                e.Handled = true;
                if (dgSpiders.SelectedItem != null && dgSpiders.SelectedItem.GetType() == typeof(Spider))
                {
                    Spider selectedSpider = dgSpiders.SelectedItem as Spider;
                    DeleteSpider(selectedSpider);                    
                }                                
            }
            else if(e.Key == Key.Enter)
            {
                e.Handled = true;
                if (sender != null)
                {
                    Spider selectedItem = ((DataGrid)sender).SelectedItem as Spider;
                    Globals.LastSelectedSpiderId = selectedItem.SpiderId;
                    MainWindow mainWindow = VisualElementsHelper.GetMainWindow();
                    mainWindow.gridMainContent.Children.Clear();
                    SingleSpiderDetails singleSpiderDetails = new(selectedItem);
                    mainWindow.gridMainContent.Children.Add(singleSpiderDetails);
                }                
            }
        }

        private void miAddSpider_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem.Name == "miAddSpider")
            {
                VisualElementsHelper.GetMainMenuUserControl().OpenPage(MainMenuPages.AddSpider);
            }
            else if (menuItem.Name == "miDeleteSpider")
            {
                if (dgSpiders.SelectedItem != null && dgSpiders.SelectedItem.GetType() == typeof(Spider))
                {
                    Spider selectedSpider = dgSpiders.SelectedItem as Spider;
                    DeleteSpider(selectedSpider);
                }
            }
        }

        private void DeleteSpider(Spider spider)
        {
            CustomMessageBoxResult result = CustomMessageBox.ShowYesNo(_translation.WarningCaption, _translation.ConfirmSelectedSpiderToDelete + spider.Name + " ?", CustomMessageBoxImage.Warning);
            if (result == CustomMessageBoxResult.YesOk)
            {
                Globals.LastSelectedSpiderId = -1;
                btnDeleteSpiderHelper.Command.Execute(spider);
            }
        }

        private void TranslateHeaders()
        {
            // TODO BINDING invoke error Cannot find governing FrameworkElement or FrameworkContentElement for target element
            SpiderTranslation translation = Globals.Translation.SpiderTranslation;
            nameHeader.Header = translation.Name;
            typeHeader.Header = translation.Type;
            speciesHeader.Header = translation.Species;
            birthDateHeader.Header = translation.BirthDate;
            purchaseDateHeader.Header = translation.PurchaseDate;
            lastFeedingDateHeader.Header = translation.LastFeedingDate;
            isActiveHeader.Header = translation.IsActive;
            deathDateHeader.Header = translation.DeathDate;
            sexHeader.Header = translation.Sex;
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                e.Handled = true;
                VisualElementsHelper.GetMainMenuUserControl().OpenPage(MainMenuPages.SpidersAlternativeView);
            }
        }

        private void btnSwitchView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Globals.IsSpiderAlternativeView = true;
            VisualElementsHelper.GetMainMenuUserControl().OpenPage(MainMenuPages.SpidersAlternativeView);
        }
    }
}
