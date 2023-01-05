using Microsoft.Data.Sqlite;
using Repository.Controlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public abstract class TestIntegrationBase
    {
        protected Connection connParam = new();

        protected T GetValueFromDataBaseField<T>(string tableName, string fieldName, string whereCondition = "")
        {            
            using (SqliteConnection conn = new(connParam.GetLocalConnectionString()))
            {
                conn.Open();
                using (SqliteCommand cmd = conn.CreateCommand())
                {
                    if (string.IsNullOrEmpty(whereCondition))
                    {
                        cmd.CommandText = string.Format("SELECT {0} FROM {1}", fieldName, tableName);
                    }
                    else
                    {
                        cmd.CommandText = string.Format("SELECT {0} FROM {1} WHERE {3}", fieldName, tableName, whereCondition);
                    }
                    conn.Close();
                    return ConvertFromObject<T>(cmd.ExecuteScalar(), default(T));
                }                               
            }            
        }

        private T ConvertFromObject<T>(object input, T defaultValue)
        {
            try
            {
                if (input.GetType() == typeof(T))
                {
                    return (T)input;
                }
                else
                {
                    return (T)Convert.ChangeType(input, typeof(T));
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }    
}
