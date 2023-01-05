using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Translation
{
    public class AppTranslation
    {
        public string LanguageSymbol { get; set; }
        public string LoadingText { get; set; }
        public CustomMessageBoxTranslation CustomMessageBoxTranslation { get; set; }
        public ContextMenuTranslation ContextMenuTranslation { get; set; }
        public MainMenuTranslation MainMenuTranslation { get; set; }
        public UserSettingsTranslation UserSettingsTranslation { get; set; }
        public SpiderTranslation SpiderTranslation { get; set; }
        public HomePageTranslation HomePageTranslation { get; set; }
        public SettingsTranslation SettingsTranslation { get; set; }
        public MoltTranslation MoltTranslation { get; set; }
    }
}
