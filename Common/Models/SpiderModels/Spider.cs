using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.SpiderModels
{
    public class Spider
    {
        public int UserId { get; set; }
        public int SpiderId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Species { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateOnly? PurchaseDate { get; set; }
        public DateOnly? LastFeedingDate { get; set; }
        public DateOnly? DeathDate { get; set; }
        public bool IsActive { get; set; }
        public string ImagePath { get; set; }
        public string Sex { get; set; }
        public List<Molt> MoltsList { get; set; }

        public Spider()
        {

        }

        public Spider(int userId, int spiderId, string name, string type, string species, DateOnly birthDate, DateOnly purchaseDate, DateOnly lastFeedingDate,
            DateOnly deathDate, bool isActive, string imagePath, string sex, List<Molt> moltsList)
        {
            UserId = userId;
            SpiderId = spiderId;
            Name = name;
            Type = type;
            Species = species;
            BirthDate = birthDate;
            PurchaseDate = purchaseDate;
            LastFeedingDate = lastFeedingDate;
            DeathDate = deathDate;
            IsActive = isActive;
            ImagePath = imagePath;
            Sex = sex;
            MoltsList = moltsList;
        }
    }
}
