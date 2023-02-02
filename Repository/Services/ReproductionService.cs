using Common;
using Common.Models.SpiderModels;
using Microsoft.Data.Sqlite;
using Repository.Controlers;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{   
    public class ReproductionService : IReproduction
    {
        private Connection _connParam;

        public ReproductionService(Connection connParam)
        {
            _connParam = connParam;
        }

        public bool AddReproduction(Reproduction reproduction)
        {
            try
            {
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Add("@SPIDER_FEMALE_ID", SqliteType.Integer).Value = reproduction.SpiderFemaleId;
                        cmd.Parameters.Add("@COPULATION_DATE", SqliteType.Text).Value = reproduction.CopulationDate != null ? reproduction.CopulationDate : DBNull.Value;
                        cmd.Parameters.Add("@IS_SUCCESSFUL", SqliteType.Integer).Value = reproduction.IsSuccessful != null ? reproduction.IsSuccessful : DBNull.Value;
                        cmd.Parameters.Add("@IS_SPIDER_MALE_EATEN", SqliteType.Integer).Value = reproduction.IsSpiderMaleEaten != null ? reproduction.IsSpiderMaleEaten : DBNull.Value;
                        cmd.Parameters.Add("@IS_COCCON", SqliteType.Integer).Value = reproduction.IsCoccon != null ? reproduction.IsCoccon : DBNull.Value;
                        cmd.Parameters.Add("@NOTE", SqliteType.Text).Value = reproduction.Note != null ? reproduction.Note : DBNull.Value;
                        cmd.CommandText = "INSERT INTO Reproductions " +
                            " (Spider_Female_Id, Copulation_Date, Is_Successful, Is_SPider_Male_Eaten, Is_Coccon, Note)" +
                            " VALUES (@SPIDER_FEMALE_ID, @COPULATION_DATE, @IS_SUCCESSFUL, @IS_SPIDER_MALE_EATEN, @IS_COCCON, @NOTE)";
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
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, LogType.Error, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return false;
            }
        }

        public bool DeleteAllSpiderReproductions(int spiderId)
        {
            try
            {
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Add("@SPIDER_ID", SqliteType.Integer).Value = spiderId;
                        cmd.CommandText = "DELETE FROM Reproductions WHERE Spider_Female_Id = @SPIDER_ID";
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, LogType.Error, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return false;
            }
        }

        public bool DeleteReproduction(int spiderId, string reproductionDate)
        {
            try
            {
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        DateTime formatedDate;
                        formatedDate = DateTime.ParseExact(reproductionDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        cmd.Parameters.Add("@SPIDER_ID", SqliteType.Integer).Value = spiderId;
                        cmd.Parameters.Add("@COPULATION_DATE", SqliteType.Text).Value = formatedDate.ToString("yyyy-MM-dd");
                        cmd.CommandText = "DELETE FROM Reproductions WHERE Spider_Female_Id = @SPIDER_ID AND Copulation_Date = @COPULATION_DATE";
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
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, LogType.Error, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return false;
            }
        }

        public int GetSpiderReproductionCount(int spiderId)
        {
            int reproductionCount = -1;
            try
            {
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Add("@SPIDER_ID", SqliteType.Integer).Value = spiderId;
                        cmd.CommandText = "SELECT count(Reproduction_Id) AS Reproduction_Count " +
                            "FROM Reproductions " +
                            "GROUP BY Reproduction_Id";
                        SqliteDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            reproductionCount = int.Parse(reader["Reproduction_Count"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, LogType.Error, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return -1;
            }
            return reproductionCount;
        }

        public ObservableCollection<Reproduction> GetSpiderReproductions(int spiderId)
        {
            ObservableCollection<Reproduction> reproductionsList = new ObservableCollection<Reproduction>();
            try
            {
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Add("@SPIDER_FEMALE_ID", SqliteType.Integer).Value = spiderId;
                        cmd.CommandText = "SELECT * FROM Reproductions" +
                            " WHERE Spider_Female_Id = @SPIDER_FEMALE_ID";
                        SqliteDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Reproduction reproduction = new();
                            if (!string.IsNullOrEmpty(reader["Copulation_Date"].ToString()))
                            {
                                reproduction.CopulationDate = DateOnly.ParseExact(reader["Copulation_Date"].ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            reproduction.IsSuccessful = Convert.ToBoolean((reader["Is_Successful"]));
                            reproduction.IsSpiderMaleEaten = Convert.ToBoolean(reader["Is_Spider_Male_Eaten"]);
                            reproduction.IsCoccon = Convert.ToBoolean(reader["Is_Coccon"]);
                            reproduction.Note = reader["Note"].ToString();
                            reproductionsList.Add(reproduction);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, LogType.Error, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return reproductionsList;
            }
            return reproductionsList;
        }

        //public bool UpdateReproduction(Reproduction reproduction)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
