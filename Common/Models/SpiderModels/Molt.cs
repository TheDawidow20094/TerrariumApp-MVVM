using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.SpiderModels
{
    public class Molt
    {
        public int SpiderId { get; set; }
        public int MoltId { get; set; }
        public DateOnly? MoltDate { get; set; }
        public string MoltDesc { get; set; }
        public string ImagePath { get; set; }
        public bool HasImage { get; set; }

        public Molt() //ctor
        {

        }

        public Molt(int spiderId, int moltId, DateOnly? moltDate, string moltDesc, string imagePath, bool hasImage)
        {
            SpiderId = spiderId;
            MoltId = moltId;
            MoltDate = moltDate;
            MoltDesc = moltDesc;
            ImagePath = imagePath;
            HasImage = hasImage;
        }
    }
}
