using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariumApp.Helpers
{
    public class UserImageHelper
    {
        /// <summary>
        /// Func ShowFileDialog to choose image and save this image
        /// </summary>
        /// <param name="finallyFilePath">File path</param>
        public void SaveImageAndGetImagePath(out string finallyFilePath)
        {
            string imagePath = string.Empty;
            finallyFilePath = string.Empty;
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Images(*.jpg, *.jpeg, *.png)| *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                imagePath = openFileDialog.FileName;
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "UserImages");
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                string fileName = Path.GetFileNameWithoutExtension(imagePath);
                finallyFilePath = Path.Combine(savePath, fileName + ".png");
                File.Copy(imagePath, finallyFilePath, true);
            }
        }
    }
}
