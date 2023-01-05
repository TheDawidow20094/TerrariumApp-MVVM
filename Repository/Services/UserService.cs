using Common;
using Common.Models;
using Microsoft.Data.Sqlite;
using Repository.Controlers;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class UserService : IUser
    {
        private Connection _connParam;

        public UserService(Connection connParam)
        {
            _connParam = connParam;
        }

        public User AddNewUser(string userName, string language)
        {
            User user = new();
            try
            {
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Add("@USER_NAME", SqliteType.Text).Value = userName;
                        cmd.Parameters.Add("@LANGUAGE", SqliteType.Text).Value = language;
                        cmd.CommandText = "INSERT INTO Users (User_Name, Language)" +
                            " VALUES (@USER_NAME, @LANGUAGE); SELECT last_insert_rowid() AS User_Id";
                        SqliteDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            user.UserId = int.Parse(reader["User_Id"].ToString());
                            user.UserName = userName;
                            user.Language = language;
                        }
                        else
                        {
                            return user;
                        }
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, ex.Message, LogType.Error, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return null;
            }
            return user;
        }

        
        public bool DeleteUser(int userId)
        {
            try
            {
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Add("@USER_ID", SqliteType.Integer).Value = userId;
                        cmd.CommandText = "DELETE " +
                            "FROM Users" +
                            " WHERE User_Id = @USER_ID";
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

        public User GetLocalUserData(int userId)
        {
            User user = new();
            try
            {
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Users";
                        if (userId != 0)
                        {
                            cmd.Parameters.Add("@USER_ID", SqliteType.Integer).Value = userId;
                            cmd.CommandText += " WHERE User_Id = @USER_ID ORDER BY User_Id DESC";
                        }
                        SqliteDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            user.UserId = int.Parse(reader["User_Id"].ToString());
                            user.UserName = reader["User_Name"].ToString();
                            user.Language = reader["Language"].ToString();
                        }
                        else
                        {
                            throw new Exception("No user data");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, ex.Message, LogType.CriticalError, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return null;
            }
            return user;
        }
        
        public ObservableCollection<User> GetLocalUsers()
        {
            ObservableCollection<User> users = new();            
            try
            {   using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Users";
                        SqliteDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            User user = new();
                            user.UserId = int.Parse(reader["User_Id"].ToString());
                            user.UserName = reader["User_Name"].ToString();
                            user.Language = reader["Language"].ToString();
                            users.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, ex.Message, LogType.CriticalError, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return null;
            }
            return users;
        }
    }
}
