using Common.Models.SpiderModels;
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
    /// Interaction logic for HomePageUserControl.xaml
    /// </summary>
    public partial class HomePageUserControl : UserControl
    {
        private bool _firstSelectionChangeInvokedByLoadingControl = true;

        public HomePageUserControl()
        {
            InitializeComponent();
            this.DataContext = new HomePageViewModel();
        }

        private void cbSpiders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_firstSelectionChangeInvokedByLoadingControl == false)
            {
                Globals.LastSelectedSpiderId = (cbSpiders.SelectedItem as Spider).SpiderId;
                btnSpiderChangeHelper.Command.Execute(null);
            }
            else
            {
                _firstSelectionChangeInvokedByLoadingControl = false;
            }
        }

        private void imageAddSpider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (grNoSpidersInfo.IsVisible)
            {
                VisualElementsHelper.GetMainMenuUserControl().OpenPage(MainMenuPages.AddSpider);
            }
        }
    }
}
