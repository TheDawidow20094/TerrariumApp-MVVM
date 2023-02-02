using Common.Models.SpiderModels;
using Common.Translation;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for SingleSpiderDetails.xaml
    /// </summary>
    public partial class SingleSpiderDetails : UserControl
    {
        private CustomMessageBoxTranslation _translation = Globals.Translation.CustomMessageBoxTranslation;
        private Spider _spider;
        private BrushConverter _converter = new();
        private bool _isEdited = false;
        private bool _successfullEditSPider = true;
        public SingleSpiderDetails(Spider spider)
        {
            InitializeComponent();
            _spider = spider;
            SingleSpiderDetailsViewModel viewModel = new(spider);
            this.DataContext = viewModel;
            SetSpiderImageIfExist();

            viewModel.NonSuccessfullEditSPider = () =>
            {
                _successfullEditSPider = false;
                CustomMessageBox.ShowOK(_translation.ErrorCaption, _translation.SpiderUpdatingDataError, CustomMessageBoxImage.Error);
            };
        }

        private void SetSpiderImageIfExist()
        {
            if (File.Exists(_spider.ImagePath))
            {
                imageSpiderImage.Source = new BitmapImage(new Uri(_spider.ImagePath));
            }
            else
            {
                Globals.Log.WriteLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Image file not found!", Common.LogType.ImportantMessage, Globals.LocalUserData.UserId, Globals.LocalUserData.UserName);
                imageSpiderImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/addPhoto.png"));
            }
        }

        private void TextBoxes_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;            
            switch (textBox.Name)
            {
                case "tboxName":
                    if (string.IsNullOrWhiteSpace(tboxName.Text))
                    {
                        tboxName.Background = Globals.RedColor;
                        return;
                    }
                    else
                    {
                        tboxName.Background = Globals.ControlsColor;
                    }
                    if (tboxName.Text != _spider.Name)
                    {
                        tboxName.BorderBrush = (Brush)_converter.ConvertFromString("#FFdbf022");
                        _isEdited = true;
                    }
                    else
                    {
                        tboxName.BorderBrush = Brushes.White;
                    }
                    break;
                case "tboxType":
                    if (string.IsNullOrWhiteSpace(tboxType.Text))
                    {
                        tboxType.Background = Globals.RedColor;
                        return;
                    }
                    else
                    {
                        tboxType.Background = Globals.ControlsColor;
                    }
                    if (tboxType.Text != _spider.Type)
                    {
                        tboxType.BorderBrush = (Brush)_converter.ConvertFromString("#FFdbf022");
                        _isEdited = true;
                    }
                    else
                    {
                        tboxType.BorderBrush = Brushes.White;
                    }
                    break;
            }            
        }

        private void cbSpecies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSpecies.SelectedItem.ToString() != _spider.Species)
            {
                cbSpecies.BorderBrush = (Brush)_converter.ConvertFromString("#FFdbf022");
                _isEdited = true;
            }
            else
            {
                cbSpecies.BorderBrush = Brushes.Transparent;
            }
        }

        private void cbSex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((cbSex.SelectedItem as ComboBoxItem).Content.ToString() != _spider.Sex.ToString())
            {
                cbSex.BorderBrush = (Brush)_converter.ConvertFromString("#FFdbf022");
                _isEdited = true;
            }
            else
            {
                cbSex.BorderBrush = Brushes.Transparent;
            }
        }

        private void DatePickers_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            string trimedDate = datePicker.SelectedDate.ToString().Substring(0, 10);
            switch (datePicker.Name)
            {
                case "dpBirthDate":
                    if (DateOnly.Parse(trimedDate) != _spider.BirthDate)
                    {
                        dpBirthDate.BorderBrush = (Brush)_converter.ConvertFromString("#FFdbf022");
                        _isEdited = true;
                    }
                    else
                    {
                        dpBirthDate.BorderBrush = Brushes.White;
                    }
                    break;
                case "dpPutchaseDate":
                    if (DateOnly.Parse(trimedDate) != _spider.PurchaseDate)
                    {
                        dpPutchaseDate.BorderBrush = (Brush)_converter.ConvertFromString("#FFdbf022");
                        _isEdited = true;
                    }
                    else
                    {
                        dpPutchaseDate.BorderBrush = Brushes.White;
                    }
                    break;
                case "dpLastFeedingDate":
                    if (DateOnly.Parse(trimedDate) != _spider.LastFeedingDate)
                    {
                        dpLastFeedingDate.BorderBrush = (Brush)_converter.ConvertFromString("#FFdbf022");
                        _isEdited = true;
                    }
                    else
                    {
                        dpLastFeedingDate.BorderBrush = Brushes.White;
                    }
                    break;
                case "dpDeathDate":
                    if (DateOnly.Parse(trimedDate) != _spider.DeathDate)
                    {
                        dpDeathDate.BorderBrush = (Brush)_converter.ConvertFromString("#FFdbf022");
                        _isEdited = true;
                    }
                    else
                    {
                        dpDeathDate.BorderBrush = Brushes.White;
                    }
                    break;
            }            
        }

        private void btnEditSpiderAndExit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CanUpdate())
            {
                Button button = sender as Button;
                UpdateSpider();
                Globals.LastSelectedSpiderId = _spider.SpiderId;
                button.Command.Execute(_spider);
                if (_successfullEditSPider)
                {
                    VisualElementsHelper.GetMainMenuUserControl().OpenPage(MainMenuPages.Spiders);
                }
            }            
        }

        private bool CanUpdate()
        {
            bool result = true;
            if(string.IsNullOrWhiteSpace(tboxName.Text))
            {
                tboxName.Background = Globals.RedColor;
                return false;
            }
            if (string.IsNullOrWhiteSpace(tboxType.Text))
            {
                tboxType.Background = Globals.RedColor;
                return false;
            }
            if (cbSpecies.SelectedItem == null || string.IsNullOrWhiteSpace(cbSpecies.SelectedItem.ToString()))
            {
                cbSpecies.BorderBrush = Globals.RedColor;
                return false;
            }
            return result;
        }

        private void UpdateSpider()
        {
            string trimedDate = string.Empty;
            _spider.Name = tboxName.Text;
            _spider.Type = tboxType.Text;
            _spider.Sex = (cbSex.SelectedItem as ComboBoxItem).Content.ToString();
            _spider.Species = cbSpecies.SelectedValue.ToString();
            if (dpBirthDate.SelectedDate != null)
            {
                trimedDate = dpBirthDate.SelectedDate.ToString().Substring(0, 10);
                _spider.BirthDate = DateOnly.Parse(trimedDate);
            }
            if (dpPutchaseDate.SelectedDate != null)
            {
                trimedDate = dpPutchaseDate.SelectedDate.ToString().Substring(0, 10);
                _spider.PurchaseDate = DateOnly.Parse(trimedDate);
            }
            if (dpLastFeedingDate.SelectedDate != null)
            {
                trimedDate = dpLastFeedingDate.SelectedDate.ToString().Substring(0, 10);
                _spider.LastFeedingDate = DateOnly.Parse(trimedDate);
            }
            if (dpDeathDate.SelectedDate != null)
            {
                trimedDate = dpDeathDate.SelectedDate.ToString().Substring(0, 10);
                _spider.DeathDate = DateOnly.Parse(trimedDate);
            }
            _spider.IsActive = (bool)tgIsActive.IsChecked;
        }

        private void imageSpiderImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string imagePath = string.Empty;
            UserImageHelper imageHelper = new();
            imageHelper.SaveImageAndGetImagePath(out imagePath);
            if (!string.IsNullOrEmpty(imagePath))
            {
                _isEdited = true;
                _spider.ImagePath = imagePath;
                imageSpiderImage.Source = new BitmapImage(new Uri(imagePath));
            }
        }
    }
}
