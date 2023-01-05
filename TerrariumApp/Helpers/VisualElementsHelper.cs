using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace TerrariumApp.Helpers
{
    public static class VisualElementsHelper
    {
        /// <summary>
        /// Function returns all parents children
        /// </summary>
        /// <typeparam name="T">Searching children type</typeparam>
        /// <param name="depObj">parent</param>
        /// <returns></returns>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        /// <summary>
        /// Func returns mainWindow
        /// </summary>
        /// <returns></returns>
        public static MainWindow GetMainWindow()
        {
            return Application.Current.MainWindow as MainWindow;
        }

        /// <summary>
        /// Func returns MainMenu
        /// </summary>
        /// <returns></returns>
        public static MainMenuUserControl GetMainMenuUserControl()
        {
            var childrens = GetMainWindow().gridMainMenu.Children;
            foreach (var item in childrens)
            {
                return (MainMenuUserControl)item;
            }
            return null;
        }

        /// <summary>
        /// Func reload main controls like MainMenu, App footer ... and go to Home page (should be called when app language changes)
        /// </summary>
        public static void ReloadMainAppControls()
        {
            MainWindow mainWindow = GetMainWindow();
            mainWindow.gridMainMenu.Children.Clear();
            mainWindow.gridFooter.Children.Clear();
            mainWindow.gridMainContent.Children.Clear();
            mainWindow.gridMainMenu.Children.Add(new MainMenuUserControl());
            mainWindow.gridFooter.Children.Add(new AppFooterUserControl());
            GetMainMenuUserControl().OpenPage(MainMenuPages.HomePage);
            Globals.Log.WriteLog("VisualElementsHelper", "Reloading main app controls", Common.LogType.ImportantMessage, Globals.LocalUserData.UserId, Globals.LocalUserData.UserName);
        }
    }
}
