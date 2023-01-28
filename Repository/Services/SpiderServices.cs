using Common.Models.SpiderModels;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json.Linq;
using Repository.Controlers;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class SpiderServices : ISpider
    {
        private Connection _connParam;

        public SpiderServices(Connection connection)
        {
            _connParam = connection;
        }

        public bool AddSpider(Spider spiderToAdd, int userId)
        {
            try
            {
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Add("@USER_ID", SqliteType.Integer).Value = spiderToAdd.UserId;
                        cmd.Parameters.Add("@NAME", SqliteType.Text).Value = spiderToAdd.Name != null ? spiderToAdd.Name : DBNull.Value;
                        cmd.Parameters.Add("@TYPE", SqliteType.Text).Value = spiderToAdd.Type != null ? spiderToAdd.Type : DBNull.Value;
                        cmd.Parameters.Add("@SPECIES", SqliteType.Text).Value = spiderToAdd.Species != null ? spiderToAdd.Species : DBNull.Value;
                        cmd.Parameters.Add("@BIRTH_DATE", SqliteType.Text).Value = spiderToAdd.BirthDate != null ? spiderToAdd.BirthDate : DBNull.Value;
                        cmd.Parameters.Add("@PURCHASE_DATE", SqliteType.Text).Value = spiderToAdd.PurchaseDate != null ? spiderToAdd.PurchaseDate : DBNull.Value;
                        cmd.Parameters.Add("@LAST_FEEDING_DATE", SqliteType.Text).Value = spiderToAdd.LastFeedingDate != null ? spiderToAdd.LastFeedingDate : DBNull.Value;
                        cmd.Parameters.Add("@IS_ACTIVE", SqliteType.Integer).Value = spiderToAdd.IsActive;
                        cmd.Parameters.Add("@IMAGE_PATH", SqliteType.Text).Value = spiderToAdd.ImagePath != null ? spiderToAdd.ImagePath : DBNull.Value;
                        cmd.Parameters.Add("@SEX", SqliteType.Text).Value = spiderToAdd.Sex != null ? spiderToAdd.Sex : DBNull.Value;

                        cmd.CommandText = "INSERT INTO Spiders " +
                            " (User_Id, Name, Type, Species, Birth_Date, Purchase_date, Last_Feeding_Date, Is_Active, Image_Path, Sex)" +
                            " VALUES (@USER_ID, @NAME, @TYPE, @SPECIES, @BIRTH_DATE, @PURCHASE_DATE, @LAST_FEEDING_DATE, @IS_ACTIVE, @IMAGE_PATH, @SEX)";
                        if (cmd.ExecuteNonQuery() != 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, ex.Message, Common.LogType.CriticalError, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return false;
            }
        }

        public bool DeleteAllUserSpiders(int userId)
        {
            try
            {
                ObservableCollection<Spider> userSpiders = GetUserSpiders(userId);
                userSpiders.ToList().ForEach(s =>
                {
                    DeleteSpider(s.SpiderId, userId); //TODO : must optymalize (using one sql, DELETE CASCADE)
                });
            }
            catch (Exception ex)
            {
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, ex.Message, Common.LogType.CriticalError, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return false;
            }
            return true;
        }       

        public bool DeleteSpider(int spiderId, int userId)
        {
            try
            {
                //We must delete Molts and Reproductions before deleting spider!
                DeleteMoltsAndReproductions(spiderId);
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Add("@SPIDER_ID", SqliteType.Integer).Value = spiderId;
                        cmd.Parameters.Add("@USER_ID", SqliteType.Integer).Value = userId;
                        cmd.CommandText = "DELETE FROM Spiders" +
                            " WHERE Spider_Id = @SPIDER_ID AND User_Id = @USER_ID";
                        if (cmd.ExecuteNonQuery() != 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, ex.Message, Common.LogType.CriticalError, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return false;
            }
        }

        public ObservableCollection<Spider> GetUserSpiders(int userId)
        {
            ObservableCollection<Spider> spiders = new();           
            try
            {
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Add("@USER_ID", SqliteType.Integer).Value = userId;
                        cmd.CommandText = "SELECT * " +
                            " FROM Spiders" +
                            " WHERE User_Id = @USER_ID";
                        SqliteDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Spider spider = new();
                            spider.UserId = int.Parse(reader["User_Id"].ToString());
                            spider.SpiderId = int.Parse(reader["Spider_Id"].ToString());
                            spider.Name = reader["Name"].ToString();
                            spider.Type = reader["Type"].ToString();
                            spider.Species = reader["Species"].ToString();
                            if (!string.IsNullOrEmpty(reader["Birth_Date"].ToString()))
                            {
                                spider.BirthDate = DateOnly.ParseExact(reader["Birth_Date"].ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            if (!string.IsNullOrEmpty(reader["Purchase_Date"].ToString()))
                            {
                                spider.PurchaseDate = DateOnly.ParseExact(reader["Purchase_Date"].ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            if (!string.IsNullOrEmpty(reader["Last_Feeding_Date"].ToString()))
                            {
                                spider.LastFeedingDate = DateOnly.ParseExact(reader["Last_Feeding_Date"].ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            if (!string.IsNullOrEmpty(reader["Death_Date"].ToString()))
                            {
                                spider.DeathDate = DateOnly.ParseExact(reader["Death_Date"].ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            spider.IsActive = Convert.ToBoolean(reader["Is_Active"]);
                            spider.ImagePath = reader["Image_Path"].ToString();
                            spider.Sex = reader["Sex"].ToString();
                            spiders.Add(spider);
                        }
                    }
                    return spiders;
                }
            }
            catch (Exception ex)
            {
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, ex.Message, Common.LogType.CriticalError, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return spiders;
            }
        }

        public int GetUserSpidersCount(int userId)
        {
            int count = 0;
            try
            {
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Add("@USER_ID", SqliteType.Integer).Value = userId;
                        cmd.CommandText = "SELECT count(*) AS COUNT" +
                            " FROM Spiders" +
                            " WHERE User_Id = @USER_ID";
                        SqliteDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            count = int.Parse(reader["COUNT"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, ex.Message, Common.LogType.CriticalError, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return count;
            }
            return count;
        }

        public bool UpdateSpider(Spider spider, int userID)
        {
            try
            {
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Add("@USER_ID", SqliteType.Integer).Value = userID;
                        cmd.Parameters.Add("@SPIDER_ID", SqliteType.Integer).Value = spider.SpiderId;
                        cmd.Parameters.Add("@NAME", SqliteType.Text).Value = spider.Name;
                        cmd.Parameters.Add("@TYPE", SqliteType.Text).Value = spider.Type;
                        cmd.Parameters.Add("@SPECIES", SqliteType.Text).Value = spider.Species;
                        cmd.Parameters.Add("@BIRTH_DATE", SqliteType.Text).Value = spider.BirthDate != null ? spider.BirthDate : DBNull.Value;
                        cmd.Parameters.Add("@PURCHASE_DATE", SqliteType.Text).Value = spider.PurchaseDate != null ? spider.PurchaseDate : DBNull.Value;
                        cmd.Parameters.Add("@LAST_FEEDING_DATE", SqliteType.Text).Value = spider.LastFeedingDate != null ? spider.LastFeedingDate : DBNull.Value;
                        cmd.Parameters.Add("@DEATH_DATE", SqliteType.Text).Value = spider.DeathDate != null ? spider.DeathDate : DBNull.Value;
                        cmd.Parameters.Add("@IS_ACTIVE", SqliteType.Integer).Value = spider.IsActive;
                        cmd.Parameters.Add("@IMAGE_PATH", SqliteType.Text).Value = spider.ImagePath != null ? spider.ImagePath : DBNull.Value;
                        cmd.Parameters.Add("@SEX", SqliteType.Text).Value = spider.Sex != null ? spider.Sex : DBNull.Value;
                        cmd.CommandText = "UPDATE Spiders" +
                            " SET User_Id = @USER_ID, Spider_Id = @SPIDER_ID, Name = @NAME, Type = @TYPE, Species = @SPECIES, Purchase_Date = @PURCHASE_DATE, " +
                            " Last_Feeding_Date = @LAST_FEEDING_DATE, Death_Date = @DEATH_DATE, Is_Active = @IS_ACTIVE, Image_Path = @IMAGE_PATH, Sex = @SEX" +
                            " WHERE User_Id = @USER_ID AND Spider_Id = @SPIDER_ID";
                        if (cmd.ExecuteNonQuery() != 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, ex.Message, Common.LogType.Error, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return false;
            }
        }

        
        private void DeleteMoltsAndReproductions(int spiderId)
        {
            IMolt iMolt = new MoltServices(_connParam);
            IReproduction iReproduction = new ReproductionService(_connParam);
            iMolt.DeleteAllSpiderMolts(spiderId);
            iReproduction.DeleteAllSpiderReproductions(spiderId);
        }
    }
}
