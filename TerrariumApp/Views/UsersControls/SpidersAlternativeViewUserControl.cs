using Common.Models.SpiderModels;
using Repository.Interfaces;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
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
using TerrariumApp.Views.Windows;

namespace TerrariumApp.Views.UsersControls
{
    /// <summary>
    /// Interaction logic for SpidersALternativeViewUserControl.xaml
    /// </summary>
    public partial class SpidersAlternativeViewUserControl : UserControl
    {
        public SpidersAlternativeViewUserControl()
        {
            InitializeComponent();
            Globals.IsSpiderAlternativeView = true;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Globals.LoadingWindow.Start(VisualElementsHelper.GetMainWindow());
            PrepareLayout();
        }

        public void PrepareLayout()
        {
            int row = 0;
            SpiderServices spiderServices = new(Globals.connParam);
            ObservableCollection<Spider> spiders = spiderServices.GetUserSpiders(Globals.LocalUserData.UserId);
            Grid mainGrid = new();            
            mainGrid.Loaded += MainGrid_Loaded;
            spiders.ToList().ForEach(s =>
            {
                mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(170) });
                Border border = new();
                border.PreviewMouseLeftButtonDown += Border_PreviewMouseLeftButtonDown;
                border.Padding = new Thickness(10);
                border.Cursor = Cursors.Hand;
                Grid.SetRow(border, row);
                border.Margin = new Thickness(20, 5, 20, 5);
                border.BorderThickness = new Thickness(2);
                border.BorderBrush = Brushes.WhiteSmoke;

                Grid spiderGrid = new();
                spiderGrid.Tag = s.SpiderId;
                spiderGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(600, GridUnitType.Star) });
                spiderGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(600, GridUnitType.Star) });
                spiderGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(600, GridUnitType.Star) });
                spiderGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(200, GridUnitType.Star) });
                spiderGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(200, GridUnitType.Star) });
                spiderGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(150, GridUnitType.Star) });

                Image image = new();
                ImageSourceConverter source = new ImageSourceConverter();
                image.Height = 140;
                if (string.IsNullOrEmpty(s.ImagePath))
                {
                    image.SetValue(Image.SourceProperty, source.ConvertFromString("pack://application:,,,/Resources/Images/noImage.png"));
                }
                else
                {
                    image.SetValue(Image.SourceProperty, source.ConvertFromString(s.ImagePath));
                }                
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.Margin = new Thickness(5, 0, 0, 0);
                Grid.SetColumn(image, 0);
                Grid.SetRow(image, 0);
                Grid.SetRowSpan(image, 3);

                spiderGrid.Children.Add(image);
                spiderGrid.Children.Add(GenerateTextBlock(s.Name, 1, 0));
                spiderGrid.Children.Add(GenerateTextBlock(s.Type, 1, 1));
                spiderGrid.Children.Add(GenerateTextBlock(s.Sex, 1, 2));
                spiderGrid.Children.Add(GenerateTextBlock(Globals.Translation.SpiderTranslation.IsActive + " :", 2, 0));
                spiderGrid.Children.Add(GenerateTextBlock(s.IsActive ? Globals.Translation.CustomMessageBoxTranslation.YesButton :
                    Globals.Translation.CustomMessageBoxTranslation.NoButton, 2, 1));
                border.Child = spiderGrid;
                mainGrid.Children.Add(border);
                row++;
            });
            svMain.Content = mainGrid;
        }

        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender != null && sender.GetType() == typeof(Border))
            {
                int selectedSpiderId = (int)(((Border)sender).Child as Grid).Tag;
                Globals.LastSelectedSpiderId = selectedSpiderId;
                SpiderServices spiderServices = new(Globals.connParam);
                Spider spider = spiderServices.GetSpider(Globals.LocalUserData.UserId, selectedSpiderId);
                if (spider == null)
                {
                    CustomMessageBox.ShowOK(Globals.Translation.CustomMessageBoxTranslation.ErrorCaption, Globals.Translation.CustomMessageBoxTranslation.SpiderDetailsError,
                        CustomMessageBoxImage.Error);
                    Globals.Log.WriteLog(GetType().Name, MethodBase.GetCurrentMethod().Name, "Error to get selected spider details", Common.LogType.CriticalError, Globals.LocalUserData.UserId, Globals.LocalUserData.UserName);
                    return;
                }
                MainWindow mainWindow = VisualElementsHelper.GetMainWindow();
                mainWindow.gridMainContent.Children.Clear();
                SingleSpiderDetails singleSpiderDetails = new(spider);
                mainWindow.gridMainContent.Children.Add(singleSpiderDetails);
            }            
        }

        private TextBlock GenerateTextBlock(string text, int column, int row)
        {
            TextBlock textBlock = new();
            textBlock.Height = 150;
            textBlock.FontSize = 20;
            textBlock.Text = text;
            textBlock.FontWeight = FontWeights.Bold;
            textBlock.Foreground = Brushes.White;
            Grid.SetColumn(textBlock, column);
            Grid.SetRow(textBlock, row);
            return textBlock;
        }

        private async void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500); // that because loaded event start too fast
            Globals.LoadingWindow.Stop();
        }

        private void btnSwitchView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Globals.IsSpiderAlternativeView = false;
            VisualElementsHelper.GetMainMenuUserControl().OpenPage(MainMenuPages.Spiders);
        }
    }
}
