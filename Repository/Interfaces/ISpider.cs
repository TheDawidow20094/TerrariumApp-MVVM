using Common.Models.SpiderModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ISpider
    {
        public ObservableCollection<Spider> GetUserSpiders(int userId);        
        public bool DeleteSpider(int spiderId, int userId);
        public bool AddSpider(Spider spiderToAdd, int userId);
        public bool UpdateSpider(Spider spiderToUpdate, int userID);
        public int GetUserSpidersCount(int userId);
        /// <summary>
        /// Return false when exeption
        /// </summary>
        public bool DeleteAllUserSpiders(int userId);
    }
}
