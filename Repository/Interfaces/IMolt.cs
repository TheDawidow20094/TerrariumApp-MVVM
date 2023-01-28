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
    public interface IMolt
    {
        public bool AddMolt(Molt molt, int spiderId);        
        public ObservableCollection<Molt> GetAllMolts(int spiderId);
        public bool DeleteMolt(int moltId, int spiderId);
        public int GetSpiderMoltsCount(int spiderId);
        /// <summary>
        /// Return false when exception
        /// </summary>
        public bool DeleteAllSpiderMolts(int spiderId);
    }
}
