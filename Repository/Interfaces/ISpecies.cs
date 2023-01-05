using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ISpecies
    {
        /// <summary>
        /// Get all species in database
        /// </summary>
        /// <param name="language">App language</param>
        /// <returns></returns>
        public ObservableCollection<string> GetSpecies(string language);
    }
}
