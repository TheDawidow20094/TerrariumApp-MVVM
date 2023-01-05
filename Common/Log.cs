using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Log
    {
        public Log()
        {

        }

        /// <summary>
        /// Function generate entry in Log file
        /// </summary>
        /// <param name="className"></param>
        /// <param name="logMessage"></param>
        /// <param name="logType"></param>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        public void WriteLog(string className, string logMessage, LogType logType, int userId = -1, string userName = "")
        {
            try
            {
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                using (StreamWriter writer = File.AppendText(path + "\\" + "TerrariumAppLog.txt"))
                {
                    writer.Write(GenerateEntry(className, logMessage, logType, userId, userName));
                }
            }
            catch (Exception) { }
        }

        private string GenerateEntry(string className, string logMessage, LogType logType, int userId, string userName)
        {
            return "\n" + DateTime.Now.ToString() + "(" + className + ")" + ", [" + userId + "]" + ", UserName: " + userName + ", Type: " + logType + ", Message: " + logMessage;
        }
    }
}
