using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TerrariumApp.Views.Windows;

namespace TerrariumApp.Helpers
{
    public static class CustomMessageBox
    {
        public static CustomMessageBoxResult ShowOK(string caption, string content, CustomMessageBoxImage messageBoxImage = CustomMessageBoxImage.None)
        {
            CustomMessageBoxWindow window = new CustomMessageBoxWindow(caption, content, messageBoxImage, true);
            window.ShowDialog();
            return (CustomMessageBoxResult)window.Tag;
        }

        public static CustomMessageBoxResult ShowYesNo(string caption, string content, CustomMessageBoxImage messageBoxImage = CustomMessageBoxImage.None)
        {
            CustomMessageBoxWindow window = new CustomMessageBoxWindow(caption, content, messageBoxImage, false);
            window.ShowDialog();
            return (CustomMessageBoxResult)window.Tag;
        }
    }
}
