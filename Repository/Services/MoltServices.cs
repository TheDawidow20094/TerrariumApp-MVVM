using Common;
using Common.Models.SpiderModels;
using Microsoft.Data.Sqlite;
using Repository.Controlers;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Connection = Repository.Controlers.Connection;

namespace Repository.Services
{
    public class MoltServices : IMolt
    {
        private Connection _connParam;

        public MoltServices(Connection connParam)
        {
            _connParam = connParam;
        }

        public bool AddMolt(Molt molt,int spiderId)
        {
            try
            {
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Add("@SPIDER_ID", SqliteType.Integer).Value = spiderId;
                        cmd.Parameters.Add("@MOLT_DATE", SqliteType.Text).Value = molt.MoltDate != null ? molt.MoltDate : DBNull.Value;
                        cmd.Parameters.Add("@MOLT_DESC", SqliteType.Text).Value = molt.MoltDesc != null ? molt.MoltDesc : DBNull.Value;
                        cmd.Parameters.Add("@IMAGE_PATH", SqliteType.Text).Value = molt.ImagePath != null ? molt.ImagePath : DBNull.Value;
                        cmd.CommandText = "INSERT INTO Molts (Spider_Id, Molt_Date, Molt_Desc, Image_Path)" +
                            " VALUES ( @SPIDER_ID, @MOLT_DATE, @MOLT_DESC, @IMAGE_PATH)";
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
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, ex.Message, LogType.Error, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return false;
            }
        }

        public bool DeleteMolt(int moltId, int spiderId)
        {
            try
            {
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Add("@MOLT_ID", SqliteType.Integer).Value = moltId;
                        cmd.Parameters.Add("@SPIDER_ID", SqliteType.Integer).Value = spiderId;
                        cmd.CommandText = "DELETE FROM Molts" +
                            " WHERE Molt_Id = @MOLT_ID AND Spider_Id = @SPIDER_ID";
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
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, ex.Message, LogType.Error, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return false;
            }
        }

        public ObservableCollection<Molt> GetAllMolts(int spiderId)
        {
            ObservableCollection<Molt> molts = new();
            try
            {
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Add("@SPIDER_ID", SqliteType.Integer).Value = spiderId;
                        cmd.CommandText = " SELECT * FROM Molts" +
                            " WHERE Spider_Id = @SPIDER_ID";
                        SqliteDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Molt molt = new();
                            molt.SpiderId = int.Parse(reader["Spider_Id"].ToString());
                            molt.MoltId = int.Parse(reader["Molt_Id"].ToString());
                            if (!string.IsNullOrEmpty(reader["Molt_Date"].ToString()))
                            {
                                molt.MoltDate = DateOnly.Parse(reader["Molt_Date"].ToString());
                            }
                            molt.MoltDesc = reader["Molt_Desc"].ToString();
                            molt.ImagePath = reader["Image_Path"].ToString();
                            molts.Add(molt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, ex.Message, LogType.Error, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return molts;
            }
            return molts;
        }

        public int GetSpiderMoltsCount(int spiderId, int userId)
        {
            int count = 0;
            try
            {
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Add("@SPIDER_ID", SqliteType.Integer).Value = spiderId;
                        cmd.Parameters.Add("@USER_ID", SqliteType.Integer).Value = userId;
                        cmd.CommandText = "SELECT count(*) AS COUNT " +
                            " FROM Molts" +
                            " WHERE Spider_Id = @SPIDER_ID AND User_Id = @USER_ID";
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
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, ex.Message, LogType.Error, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return count;
            }
            return count;
        }
    }
}
