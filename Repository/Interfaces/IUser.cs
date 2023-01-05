using Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// Func get current logged user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetLocalUserData(int userId = 0);
        /// <summary>
        /// Func get all users from database
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<User> GetLocalUsers();
        /// <summary>
        /// Func delete user from database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>was successful</returns>
        public bool DeleteUser(int userId);
        /// <summary>
        /// Func add new user in database
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="language">user default language</param>
        /// <returns></returns>
        public User AddNewUser(string userName, string language);
    }
}
