using Common.Models;
using Common.Models.SpiderModels;
using NUnit.Framework;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IntegrationTests.DbDataCreatorBase;

namespace IntegrationTests
{
    [TestFixture]
    public class UserTests : PrepareDataForUserTests
    {
        [TestCase(4)]
        public void GetAllUsersAndCheckCount(int userCount)
        {
            //arrange
            var sut = new UserService(connParam);

            //act
            ObservableCollection<User> usersList = sut.GetLocalUsers();

            //assert
            Assert.AreEqual(userCount, usersList.Count);
        }

        private static readonly object[] AddNewUSerAndGetHisDataDataSource =
        {                        
            new User(4, "AnotherTestUser", "PL", null),
            new User(5, "UberUser", "PL", null),
            new User(4, "SuperUser", "EN", null),
        };

        [TestCaseSource("AddNewUSerAndGetHisDataDataSource")]
        public void AddNewUSerAndGetHisData_ShouldAddNewUstomerAndGetHisData(User userParam)
        {
            //arrange
            var sut = new UserService(connParam);

            //act
            User createdUser = sut.AddNewUser(userParam.UserName, userParam.Language);
            User downloadedUSer = sut.GetLocalUserData(createdUser.UserId);

            //assert
            Assert.AreEqual(downloadedUSer.UserId, createdUser.UserId);
            Assert.AreEqual(downloadedUSer.UserName, createdUser.UserName);
            Assert.AreEqual(downloadedUSer.Language, createdUser.Language);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void DeleteUser_ShouldDeleteUser(int userId)
        {
            //arrange
            var sut = new UserService(connParam);

            //act
            sut.DeleteUser(userId);
            ObservableCollection<User> usersList = sut.GetLocalUsers();

            //assert
            Assert.True(usersList.All(user => user.UserId != userId));
        }
    }
}
