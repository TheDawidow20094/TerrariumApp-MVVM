using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.SpiderModels
{
    public class Reproduction
    {
        public int ReproductionId { get; set; }
        public int SpiderFemaleId { get; set; }
        public DateOnly CopulationDate { get; set; }
        public bool IsSuccessful { get; set; }
        public bool IsSpiderMaleEaten { get; set; }
        public bool IsCoccon { get; set; }
        public string Note { get; set; }

        public Reproduction()
        {

        }
        public Reproduction(int reproductionId, int spiderFemaleId, DateOnly copulationDate, bool isSuccessful, bool isSpiderMaleEaten, bool isCoccon, string note)
        {
            ReproductionId = reproductionId;
            SpiderFemaleId = spiderFemaleId;
            CopulationDate = copulationDate;
            IsSuccessful = isSuccessful;
            IsSpiderMaleEaten = isSpiderMaleEaten;
            IsCoccon = isCoccon;
            Note = note;
        }
    }
}
