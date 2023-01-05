using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Controlers
{
    public static class RepositoryGlobals
    {
        public static Connection Connection = new();
        public static Log Log = new();
        public static int logUserId = -1;
        public static string logUserName = string.Empty;
    }
}
