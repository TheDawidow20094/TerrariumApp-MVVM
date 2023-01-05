using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariumApp
{
    /// <summary>
    /// Which photo will be chosen in custom message box
    /// </summary>
    public enum CustomMessageBoxImage
    {
        [Description("No image")]
        None = 0,
        [Description("Warning")]
        Warning = 1,
        [Description("Error")]
        Error = 2,
    }

    /// <summary>
    /// Returned value from custom message box
    /// </summary>
    public enum CustomMessageBoxResult
    {
        [Description("Exit")]
        Exit = 0,
        [Description("YesOk")]
        YesOk = 1,
        [Description("No")]
        No = 2,
    }

    /// <summary>
    /// Pages in main menu
    /// </summary>
    public enum MainMenuPages
    {
        [Description("Add")]
        Add = 0,
        [Description("AddSpider")]
        AddSpider = 1,
        [Description("AddMolt")]
        AddMolt = 2,
        [Description("HomePage")]
        HomePage = 3,
        [Description("Spiders")]
        Spiders = 4,
        [Description("Molts")]
        Molts = 5,
        [Description("Stats")]
        Stats = 6,
    }
}
