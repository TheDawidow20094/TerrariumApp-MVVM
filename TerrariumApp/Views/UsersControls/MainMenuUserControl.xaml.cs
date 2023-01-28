using Common;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TerrariumApp.Helpers;
using TerrariumApp.VievsModels;
using TerrariumApp.Views.UsersControls;
using TerrariumApp.Views.Windows;

namespace TerrariumApp
{
    /// <summary>
    /// Interaction logic for MainMenuUserControl.xaml
    /// </summary>
    public partial class MainMenuUserControl : UserControl
    {        
        public MainMenuUserControl()
        {
            InitializeComponent();
            this.DataContext = new MainMenuViewModel();
        }

        private void MenuButtons_Click(object sender, RoutedEventArgs e)
        {            
            ToggleButton toggleButton = sender as ToggleButton;
            HandleClickAgainThisSameButton(toggleButton);
            ChangeSelectedToggleButton(toggleButton);
            MainWindow mainWindow = VisualElementsHelper.GetMainWindow();
            switch (toggleButton.Name)
            {
                case "tbtnAdd":
                    if (spAddInnerMenu.Visibility == Visibility.Collapsed)
                    {
                        spAddInnerMenu.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        spAddInnerMenu.Visibility = Visibility.Collapsed;
                    }
                    Globals.LastOpenedPage = MainMenuPages.Add;
                    break;
                case "tbtnAddSpider":
                    tbtnAdd.IsChecked = true;
                    mainWindow.gridMainContent.Children.Clear();
                    AddSpiderUserControl addSpiderUserControl = new();
                    mainWindow.gridMainContent.Children.Add(addSpiderUserControl);                    
                    Globals.LastOpenedPage = MainMenuPages.AddSpider;
                    break;
                case "tbtbAddMolt":
                    tbtnAdd.IsChecked = true;
                    mainWindow.gridMainContent.Children.Clear();
                    AddMoltUserControl addMoltUserControl = new();
                    mainWindow.gridMainContent.Children.Add(addMoltUserControl);
                    Globals.LastOpenedPage = MainMenuPages.AddMolt;
                    break;
                case "tbtbAddReproduction":
                    tbtnAdd.IsChecked = true;
                    mainWindow.gridMainContent.Children.Clear();
                    AddReproductionUserControl addReproductionUserControl = new();
                    mainWindow.gridMainContent.Children.Add(addReproductionUserControl);
                    Globals.LastOpenedPage = MainMenuPages.AddReproduction;
                    break;
                case "tbtnHomePage":
                    spAddInnerMenu.Visibility = Visibility.Collapsed;
                    mainWindow.gridMainContent.Children.Clear();                    
                    HomePageUserControl homePageUserControl = new();
                    mainWindow.gridMainContent.Children.Add(homePageUserControl);
                    Globals.LastOpenedPage = MainMenuPages.HomePage;
                    break;
                case "tbtnSpiders":
                    spAddInnerMenu.Visibility = Visibility.Collapsed;
                    mainWindow.gridMainContent.Children.Clear();
                    SpidersUserControl spidersUserControl = new();
                    mainWindow.gridMainContent.Children.Add(spidersUserControl);
                    Globals.LastOpenedPage = MainMenuPages.Spiders;
                    break;
                case "tbtnMolts":
                    spAddInnerMenu.Visibility = Visibility.Collapsed;
                    mainWindow.gridMainContent.Children.Clear();
                    MoltsUserControl moltsUserControl = new();
                    mainWindow.gridMainContent.Children.Add(moltsUserControl);
                    Globals.LastOpenedPage = MainMenuPages.Molts;
                    break;
                case "tbtnReproductions":
                    spAddInnerMenu.Visibility = Visibility.Collapsed;
                    mainWindow.gridMainContent.Children.Clear();
                    ReproductionsUserControl reproductionsUserControl = new();
                    mainWindow.gridMainContent.Children.Add(reproductionsUserControl);
                    Globals.LastOpenedPage = MainMenuPages.Reproductions;
                    break;
                case "tbtnStats":
                    spAddInnerMenu.Visibility = Visibility.Collapsed;
                    //LoadingWindow loadingWindow = new();
                    //loadingWindow.Start("");
                    Globals.LastOpenedPage = MainMenuPages.Stats;
                    break;
            }            
        }

        private void HandleClickAgainThisSameButton(ToggleButton pressedToggleButton)
        {            
            if (pressedToggleButton.Name == "tbtnAdd" && Globals.LastOpenedPage == MainMenuPages.Add)
            {
                tbtnAdd.IsChecked = true;
            }
            else if (pressedToggleButton.Name == "tbtnAddSpider" && Globals.LastOpenedPage == MainMenuPages.AddSpider)
            {
                tbtnAdd.IsChecked = true;
                tbtnAddSpider.IsChecked = true;
            }
            else if (pressedToggleButton.Name == "tbtbAddMolt" && Globals.LastOpenedPage == MainMenuPages.AddMolt)
            {
                tbtnAdd.IsChecked = true;
                tbtbAddMolt.IsChecked = true;
            }
            else if (pressedToggleButton.Name == "tbtnHomePage" && Globals.LastOpenedPage == MainMenuPages.HomePage)
            {
                tbtnHomePage.IsChecked = true;
            }
            else if (pressedToggleButton.Name == "tbtnSpiders" && Globals.LastOpenedPage == MainMenuPages.Spiders)
            {
                tbtnSpiders.IsChecked = true;
            }
            else if (pressedToggleButton.Name == "tbtnMolts" && Globals.LastOpenedPage == MainMenuPages.Molts)
            {
                tbtnMolts.IsChecked = true;
            }
            else if (pressedToggleButton.Name == "tbtnStats" && Globals.LastOpenedPage == MainMenuPages.Stats)
            {
                tbtnStats.IsChecked = true;
            }
        }

        private void ChangeSelectedToggleButton(ToggleButton pressedToggleButton)
        {            
            IEnumerable<ToggleButton> toggleButtons = VisualElementsHelper.FindVisualChildren<ToggleButton>(mainGrid);
            toggleButtons.Where(b => b != pressedToggleButton).ToList().ForEach(b => b.IsChecked = false);           
        }

        /// <summary>
        /// Func open page from main menu
        /// </summary>
        /// <param name="pageToSelected">This page will be open</param>
        public void OpenPage(MainMenuPages pageToSelected)
        {
            switch (pageToSelected)
            {
                case MainMenuPages.Add:
                    tbtnAdd.IsChecked = true;
                    MenuButtons_Click(tbtnAdd, null);
                    break;
                case MainMenuPages.AddSpider:
                    spAddInnerMenu.Visibility = Visibility.Visible;
                    tbtnAddSpider.IsChecked = true;
                    MenuButtons_Click(tbtnAddSpider, null);
                    break;
                case MainMenuPages.AddMolt:
                    spAddInnerMenu.Visibility = Visibility.Visible;
                    tbtbAddMolt.IsChecked = true;
                    MenuButtons_Click(tbtbAddMolt, null);
                    break;
                case MainMenuPages.AddReproduction:
                    tbtbAddReproduction.IsChecked = true;
                    MenuButtons_Click(tbtbAddReproduction, null);
                    break;
                case MainMenuPages.HomePage:
                    tbtnHomePage.IsChecked = true;
                    MenuButtons_Click(tbtnHomePage, null);
                    break;
                case MainMenuPages.Spiders:
                    tbtnSpiders.IsChecked = true;
                    MenuButtons_Click(tbtnSpiders, null);
                    break;
                case MainMenuPages.Molts:
                    tbtnMolts.IsChecked = true;
                    MenuButtons_Click(tbtnMolts, null);
                    break;
                case MainMenuPages.Reproductions:
                    tbtnReproductions.IsChecked = true;
                    MenuButtons_Click(tbtnReproductions, null);
                    break;
                case MainMenuPages.Stats:
                    tbtnStats.IsChecked = true;
                    MenuButtons_Click(tbtnStats, null);
                    break;
                default:
                    break;
            }
        }
    }
}
