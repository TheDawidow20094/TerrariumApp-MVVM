using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum LogType
    {
        [Description("Important message")]
        ImportantMessage = 1,
        [Description("Warning")]
        Warning = 2,
        [Description("Error")]
        Error = 3,
        [Description("Critical error")]
        CriticalError = 4,
    }
}
