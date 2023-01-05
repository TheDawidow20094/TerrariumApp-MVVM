using Microsoft.Data.Sqlite;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{

    public abstract class DbDataCreatorBase : TestIntegrationBase
    {
        public DbDataCreatorBase()
        {
            connParam.SetLocalConnectionString("Data Source = Database/testDatabase.db");
        }

        public void ClearDatabase()
        {
            using (SqliteConnection conn = new(connParam.GetLocalConnectionString()))
            {
                conn.Open();
                using (SqliteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Spiders";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM Users";
                    cmd.ExecuteNonQuery();
                }
            }                
        }

        [SetUpFixture]
        public abstract class PrepareDataForUserTests : DbDataCreatorBase
        {
            [OneTimeSetUp]
            public void ArrangeDataForUserTests()
            {
                using (SqliteConnection conn = new(connParam.GetLocalConnectionString()))
                {
                    ClearDatabase();
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = String.Format("INSERT INTO {0} (User_Name, Language)" +
                            " VALUES ('UserTest', 'EN'), ('UserTest2', 'EN'), ('USerTest3', 'PL'), ('USerTest4', null)",
                            "Users");
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
        }

        [SetUpFixture]
        public abstract class PrepareDataForSpiderTests : DbDataCreatorBase
        {
            [OneTimeSetUp]
            public void ArrangeDataForSpiderTests()
            {
                using (SqliteConnection conn = new(connParam.GetLocalConnectionString()))
                {
                    ClearDatabase();
                    conn.Open();
                    using (SqliteCommand cmd = conn.CreateCommand())
                    {                        
                        cmd.CommandText = String.Format("INSERT INTO {0} (User_Name, Language)" +
                            " VALUES ('UserTest', 'EN'), ('UserTest2', 'EN'), ('USerTest3', 'PL'), ('USerTest4', null)",
                            "Users");
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = String.Format("INSERT INTO {0} (User_Id, Name, Type, Species, Is_Active, Sex)" +
                            " VALUES (1, 'Tarantulla', 'Tarantulla', 'Ground', 1, 'F'), " +
                            " (1, 'Tarantulla', 'Tarantulla', 'Ground', 1, 'F')," +
                            " (1, 'Ptasznik', 'Ptasznik', 'Arboreal', 1, 'M')," +
                            " (3, 'Skakun', 'Skakun', 'Ground', 0, 'F') ",
                            "Spiders");
                        cmd.ExecuteNonQuery();

                    }
                    conn.Close();
                }
            }
        }
    }
}
