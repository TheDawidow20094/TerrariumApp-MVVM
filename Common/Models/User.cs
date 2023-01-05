using Common.Models.SpiderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Language { get; set; }
        public List<Spider> SpidersList { get; set; }

        public User()
        {

        }
        public User(int userId, string userName, string language, List<Spider> spidersList)
        {
            UserId = userId;
            UserName = userName;
            Language = language;
            SpidersList = spidersList;
        }
    }
}
