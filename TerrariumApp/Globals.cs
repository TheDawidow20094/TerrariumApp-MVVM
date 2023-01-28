using Common;
using Common.Models;
using Repository.Interfaces;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrariumApp.Helpers;
using Newtonsoft.Json;
using System.Windows.Media;
using Common.Translation;
using Common.Models.SpiderModels;
using Repository.Controlers;

namespace TerrariumApp
{
    public static class Globals
    {        
        public static Log Log = new();
        public static Connection connParam = new();
        public static ApplicationConfig ApplicationConfig = GetApplicationConfig();  
        public static AppTranslation Translation = GetApplicationTranslation(ApplicationConfig.AppLanguage);
        private static IUser _IUser = new UserService(connParam);
        public static User LocalUserData = _IUser.GetLocalUserData(ApplicationConfig.LastLoggedUserId);
        public static MainMenuPages LastOpenedPage;
        public static int LastSelectedSpiderId = -1; // this property is using to select in app pages last selected spider (last editing, last added molt ...)

        public static readonly Brush PrimaryAppColor = GetBrushColor("pirmaryAppColor");
        public static readonly Brush ControlsColor = GetBrushColor("controlsColor");
        public static readonly Brush RedColor = GetBrushColor("redColor");

        private static ApplicationConfig GetApplicationConfig()
        {            
            try
            {                
                string jsonString = File.ReadAllText("Configs/TerrariumAppConfig.json");
                return JsonConvert.DeserializeObject<ApplicationConfig>(jsonString);
            }
            catch (Exception ex)
            {
                Log.WriteLog("Globals", ex.Message, LogType.CriticalError);
                throw;
            }
        }

        private static AppTranslation GetApplicationTranslation(string language)
        {
            string jsonString = string.Empty;
            try
            {                
                switch (language)
                {
                    case "PL":
                        jsonString = File.ReadAllText("Configs/Languages/polish.json");
                        break;
                    case "EN":
                        jsonString = File.ReadAllText("Configs/Languages/english.json");
                        break;
                    default:
                        throw new Exception("Language no found!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog("Globals", ex.Message, LogType.CriticalError);
                throw;
            }
            return JsonConvert.DeserializeObject<AppTranslation>(jsonString);
        }

        private static Brush GetBrushColor(string colorType)
        {
            BrushConverter converter = new BrushConverter();
            switch (colorType)
            {
                case "pirmaryAppColor":
                    return (Brush)converter.ConvertFromString("#FF4f4a4d");
                case "controlsColor": 
                    return (Brush)converter.ConvertFromString("#FF877f84");
                case "redColor":
                    return (Brush)converter.ConvertFromString("#FF9c404f");
            }
            return null;
        }

        /// <summary>
        /// Func set new language in App config, and get new translation for application
        /// </summary>
        /// <param name="language"></param>
        public static void SetNewLanguage(string language, bool reloadMainAppControls = true)
        {
            ApplicationConfig.AppLanguage = language;
            ApplicationConfig.SerializeObject();
            Translation = GetApplicationTranslation(language);
            if (reloadMainAppControls)
            {
                VisualElementsHelper.ReloadMainAppControls();
            }            
        }
    }
}
