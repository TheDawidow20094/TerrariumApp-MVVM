using Common.Models.SpiderModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IReproduction
    {
        public bool AddReproduction(Reproduction reproduction);
        //public bool UpdateReproduction(Reproduction reproduction);
        public bool DeleteReproduction(int spiderId, string reproductionDate);
        public int GetSpiderReproductionCount(int spiderId);
        public ObservableCollection<Reproduction> GetSpiderReproductions(int spiderId);
        /// <summary>
        /// Return false when exception
        /// </summary>
        public bool DeleteAllSpiderReproductions(int spiderId);
    }
}
