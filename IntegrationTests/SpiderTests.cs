using Common.Models;
using Common.Models.SpiderModels;
using NUnit.Framework;
using Repository.Interfaces;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IntegrationTests.DbDataCreatorBase;

namespace IntegrationTests
{
    [TestFixture]
    public class SpiderTests : PrepareDataForSpiderTests
    {

        private static readonly object[] GetAllUsersSpidersDataSource =
        {
            new object[] { 1, 3, GetExpectesUserSPiderList(1)},
            new object[] { 3, 1, GetExpectesUserSPiderList(3)},
        };

        private static ObservableCollection<Spider> GetExpectesUserSPiderList(int userId)
        {
            ObservableCollection<Spider> spiderList = new();
            switch (userId)
            {
                case 1:
                    Spider spider = new()
                    {
                        Name = "Tarantulla",
                        Species = "Ground",
                        Type = "Tarantulla",
                        IsActive = true,
                        Sex = "F"
                    };
                    Spider spider2 = new()
                    {
                        Name = "Tarantulla", 
                        Species = "Ground",
                        Type = "Tarantulla",
                        IsActive = true,
                        Sex = "F"
                    };
                    Spider spider3 = new()
                    {
                        Name = "Ptasznik",
                        Species = "Arboreal",
                        Type = "Ptasznik",
                        IsActive = true,
                        Sex = "M"
                    };
                    spiderList.Add(spider);
                    spiderList.Add(spider2);
                    spiderList.Add(spider3);
                    break;
                case 3:
                    Spider spider4 = new()
                    {
                        Name = "Skakun",
                        Species = "Ground",
                        Type = "Skakun",
                        IsActive = false,
                        Sex = "F"
                    };
                    spiderList.Add(spider4);
                    break;
            }
            return spiderList;
        }

        [TestCaseSource("GetAllUsersSpidersDataSource")]
        public void GetAllUserSpiders_ShouldReturnedUserSpiders(int userId, int expectedSpiderCount, ObservableCollection<Spider> expectedUserSpiders)
        {
            //arrange
            var sut = new SpiderServices(connParam);

            //act
            ObservableCollection<Spider> spidersList = sut.GetUserSpiders(userId);
            int spidersCount = sut.GetUserSpidersCount(userId);

            //assert
            Assert.AreEqual(spidersList.Count, expectedSpiderCount);
            Assert.AreEqual(spidersList.Count, spidersCount);
            int i = 0;
            foreach (Spider spider in spidersList)
            {                
                Assert.AreEqual(spider.Name, expectedUserSpiders[i].Name);
                Assert.AreEqual(spider.Sex, expectedUserSpiders[i].Sex);
                Assert.AreEqual(spider.Species, expectedUserSpiders[i].Species);
                Assert.AreEqual(spider.Type, expectedUserSpiders[i].Type);
                i++;
            }
        }
    }
}
