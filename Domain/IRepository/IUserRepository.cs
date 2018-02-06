using Sattelite.Entities;
using System.Collections.Generic;

namespace Sattelite.EntityFramework.Repository
{
    public interface IUserRepository
    {
        User GetById(int userId);

        User GetUserByUserName(string userName);

        IEnumerable<User> GetUsers();

        bool ValidateUser(string userName, string password);

        int CreateUser(string userName, string displayName, string password, string email, int role, string createdBy);

        int CreateUser(User user);

        bool UpdateUser(User user);

        /// <summary>
        /// Create or update user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool SaveUser(User user);

        bool DeleteUser(User user);

        bool DeleteUser(int userId);
    }
}