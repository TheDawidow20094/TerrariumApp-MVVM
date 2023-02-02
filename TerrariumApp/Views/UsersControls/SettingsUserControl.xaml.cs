using Common.Models;
using Common.Translation;
using SQLitePCL;
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

namespace TerrariumApp.Views.UsersControls
{
    /// <summary>
    /// Interaction logic for SettingsUserControl.xaml
    /// </summary>
    public partial class SettingsUserControl : UserControl
    {
        private string _selectedLanguage = string.Empty;
        public SettingsUserControl()
        {
            InitializeComponent();
            Translate();
            SelectCurrentLanguage();
        }

        private void Translate()
        {
            SettingsTranslation translation = Globals.Translation.SettingsTranslation;
            tblLanguage.Text = translation.Language;
            btnConfirm.Content = translation.ConfirmButton;
            tbSpidersAltViewText.Text = translation.SpiderAltView;
        }

        private void SelectCurrentLanguage()
        {
            BrushConverter converter = new();
            switch (Globals.ApplicationConfig.AppLanguage)
            {
                case "PL":
                    btnPolishLanguage.BorderBrush = (Brush)converter.ConvertFromString("#ff5ca163");
                    break;
                case "EN":
                    btnEnglishLanguage.BorderBrush = (Brush)converter.ConvertFromString("#ff5ca163");
                    break;
            }
        }

        private void LanguageButtons_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            BrushConverter converter = new();
            Button button = sender as Button;
            IEnumerable<Button> buttons = VisualElementsHelper.FindVisualChildren<Button>(spLanguage);
            buttons.ToList().ForEach(b => b.BorderBrush = (b == button) ? (Brush)converter.ConvertFromString("#ff5ca163") : null);
            switch (button.Name)
            {
                case "btnPolishLanguage":
                    _selectedLanguage = "PL";
                    break;
                case "btnEnglishLanguage":
                    _selectedLanguage = "EN";
                    break;
            }
        }

        private void btnConfirm_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Globals.IsSpiderAlternativeView = (bool)tbSpiderAltView.IsChecked;
            Globals.ApplicationConfig.SpiderAlternativeView = (bool)tbSpiderAltView.IsChecked;
            Globals.ApplicationConfig.SerializeObject();
            if (!string.IsNullOrEmpty(_selectedLanguage))
            {
                Globals.SetNewLanguage(_selectedLanguage);
            }
            else
            {
                VisualElementsHelper.GetMainMenuUserControl().OpenPage(MainMenuPages.HomePage);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tbSpiderAltView.IsChecked = Globals.ApplicationConfig.SpiderAlternativeView;
        }
    }
}
