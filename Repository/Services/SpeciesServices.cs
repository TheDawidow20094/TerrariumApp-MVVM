using Microsoft.Data.Sqlite;
using Repository.Controlers;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class SpeciesServices : ISpecies
    {
        private Connection _connParam;

        public SpeciesServices(Connection connParam)
        {
            _connParam = connParam;
        }

        public ObservableCollection<string> GetSpecies(string language)
        {
            ObservableCollection<string> speciesList = new();
            try
            {
                using (SqliteConnection conn = new(_connParam.GetLocalConnectionString()))
                {
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Add("@LANGUAGE", SqliteType.Text).Value = language;
                        cmd.CommandText = "SELECT Species " +
                            " FROM Spiders_Species" +
                            " WHERE Language = @LANGUAGE";
                        SqliteDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string species = reader["Species"].ToString();
                            speciesList.Add(species);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RepositoryGlobals.Log.WriteLog(this.GetType().Name, ex.Message, Common.LogType.CriticalError, RepositoryGlobals.logUserId, RepositoryGlobals.logUserName);
                return speciesList;
            }
            return speciesList;
        }
    }
}
