using Common.Models.SpiderModels;
using Common.Translation;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using TerrariumApp.Helpers;
using TerrariumApp.VievsModels;
using Path = System.IO.Path;

namespace TerrariumApp.Views.UsersControls
{
    /// <summary>
    /// Interaction logic for AddSpiderUserControl.xaml
    /// </summary>
    public partial class AddSpiderUserControl : UserControl
    {
        private bool _skipCheckingAfterAddNewSpider = false;
        private CustomMessageBoxTranslation _translation = Globals.Translation.CustomMessageBoxTranslation;
        private string _imagePath;
        private bool _spiderWasAddedSuccessfull = true;

        public AddSpiderUserControl()
        {
            InitializeComponent();            
            AddSpiderViewModel viewModel = new();
            this.DataContext = viewModel;

            viewModel.ShowMessageBoxSpiderAddFailed = () =>
            {
                _spiderWasAddedSuccessfull = false;
                CustomMessageBox.ShowOK(_translation.ErrorCaption, _translation.SpiderAddingError, CustomMessageBoxImage.Error);
            };
        }

        private void btnAddSpider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (CanAddSpider())
            {
                Spider spider = new();
                spider.UserId = Globals.LocalUserData.UserId;
                spider.Name = tbxName.Text;
                spider.Type = tbxType.Text;
                if (cboxSpecies.SelectedItem != null)
                {
                    spider.Species = cboxSpecies.SelectedItem.ToString();
                }
                if (dpBirthDate.SelectedDate != null)
                {
                    spider.BirthDate = DateOnly.FromDateTime(dpBirthDate.SelectedDate.Value);
                }
                if (dpPurchaseDate.SelectedDate != null)
                {
                    spider.PurchaseDate = DateOnly.FromDateTime(dpPurchaseDate.SelectedDate.Value);
                }
                if (dpLastFeedninDate.SelectedDate != null)
                {
                    spider.LastFeedingDate = DateOnly.FromDateTime(dpLastFeedninDate.SelectedDate.Value);
                }
                if (!string.IsNullOrEmpty(_imagePath))
                {
                    spider.ImagePath = _imagePath;
                }
                spider.IsActive = (bool)tbIsActive.IsChecked;
                spider.Sex = (cbSex.SelectedItem as ComboBoxItem).Content.ToString();
                Button button = sender as Button;
                button.Command.Execute(spider);
                if (_spiderWasAddedSuccessfull == false)
                {                    
                    return;
                }
                else
                {
                    if ((bool)tbAddManyMode.IsChecked)
                    {
                        ClearValues();
                        _spiderWasAddedSuccessfull = true;
                    }
                    else
                    {
                        VisualElementsHelper.GetMainMenuUserControl().OpenPage(MainMenuPages.Spiders);
                    }                    
                }               
            }            
        }

        /// <summary>
        /// Func check is possible to add spiders and set empty fields properties
        /// </summary>
        /// <returns>Can add spider</returns>
        private bool CanAddSpider()
        {
            bool canAddSpider = true;
            if (string.IsNullOrWhiteSpace(tbxName.Text))
            {
                tbxName.Background = Globals.RedColor;
                canAddSpider = false;                
            }
            if (string.IsNullOrWhiteSpace(tbxType.Text))
            {
                tbxType.Background = Globals.RedColor;
                canAddSpider = false;
            }
            if (cboxSpecies.SelectedItem == null)
            {
                canAddSpider = false;
            }
            return canAddSpider;
        }

        private void ClearValues()
        {
            _skipCheckingAfterAddNewSpider = true;
            tbxName.Text = string.Empty;
            tbxType.Text = string.Empty;
            dpBirthDate.SelectedDate = null;
            dpPurchaseDate.SelectedDate = null;
            dpLastFeedninDate.SelectedDate = null;
            imageSpiderImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/addPhoto.png"));
            tbIsActive.IsChecked = true;
            _skipCheckingAfterAddNewSpider = false;
        }

        private void borderSpiderImage_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            string imagePath = string.Empty;
            UserImageHelper imageHelper = new();
            imageHelper.SaveImageAndGetImagePath(out imagePath);
            if (!string.IsNullOrEmpty(imagePath))
            {
                _imagePath = imagePath;
                imageSpiderImage.Source = new BitmapImage(new Uri(imagePath));
            }
        }        

        private void TextBoxes_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_skipCheckingAfterAddNewSpider == false)
            {
                TextBox textBox = sender as TextBox;
                switch (textBox.Name)
                {
                    case "tbxName":
                        if (string.IsNullOrWhiteSpace(tbxName.Text))
                        {
                            tbxName.Background = Globals.RedColor;
                        }
                        else
                        {
                            tbxName.Background = Globals.ControlsColor;
                        }
                        break;
                    case "tbxType":
                        if (string.IsNullOrWhiteSpace(tbxType.Text))
                        {
                            tbxType.Background = Globals.RedColor;
                        }
                        else
                        {
                            tbxType.Background = Globals.ControlsColor;
                        }
                        break;
                }
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            VisualElementsHelper.GetMainMenuUserControl().OpenPage(MainMenuPages.Spiders);          
        }
    }
}
