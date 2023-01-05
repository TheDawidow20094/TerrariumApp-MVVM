using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class ApplicationConfig
    {
        public string AppVersion { get; set; }
        public int LastLoggedUserId { get; set; }
        public string AppLanguage { get; set; }

        /// <summary>
        /// Func serizlize and save properties of this object
        /// </summary>
        public void SerializeObject()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this);
                using (StreamWriter sw = new StreamWriter("Configs/TerrariumAppConfig.json"))
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(writer, this);
                    }
                }
            }
            catch (Exception ex)
            {
                Log log = new Log();
                log.WriteLog(this.GetType().Name, ex.Message, LogType.Error);
            }            
        }
    }
    
}
