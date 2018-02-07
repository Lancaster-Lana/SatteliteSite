using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Sattelite.Entities;
using Sattelite.Entities.UserAgg;
using Sattelite.Framework.Encyption;
using Sattelite.Framework.Encyption.Impl;

namespace Sattelite.EntityFramework.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IEncrypting _encryptor;

        public UserRepository() : this(DependencyResolver.Current.GetService<Encryptor>())
        {
        }

        public UserRepository(IEncrypting encryptor)
            : this(DependencyResolver.Current.GetService<SatteliteDBContext>(), encryptor)
        {
        }

        public UserRepository(SatteliteDBContext context, IEncrypting encryptor)
            : base(context)
        {
            _encryptor = encryptor;
        }

        /// <summary>
        /// Find user with related entities
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetById(int userId)
        {
            return GetOne(x => x.Id == userId,
                           s => s.Subscriptions);
        }

        public User GetUserByUserName(string userName)
        {
            return GetOne(x => x.UserName.Equals(userName, StringComparison.InvariantCulture));
        }

        public IEnumerable<User> GetUsers()
        {
            var users = GetQuery<User>()
                 //.Include(x => x.Subscriptions) //Include sub -entities
                 .ToList();

            return users;
        }

        public bool ValidateUser(string userName, string password)
        {
            var user = GetUserByUserName(userName);

            if (user == null)
                return false;

            return user.UserName.Equals(userName, StringComparison.InvariantCulture)
                   && user.Password.Equals(password, StringComparison.InvariantCulture);
        }

        #region Redundant methods (but for exteral logins)

        public int CreateUser(string userName, string displayName, string password, string email, int role, string createdBy)
        {
            var hashPassword = _encryptor.Encode(password);
            var user = UserFactory.Create(userName, displayName, hashPassword, email, role, createdBy);
            return CreateUser(user);
        }

        public int CreateUser(User user)
        {
            return Save(user).Id;
        }

        public bool UpdateUser(User user)
        {
            //Update(user); //todo: update permissions
            Save(user);
            return true;
        }

        #endregion

        public bool SaveUser(User user)
        {
            Save(user);
            return true;
        }

        public bool DeleteUser(int userId)
        {
            var user = GetById(userId);
            return DeleteUser(user);
        }

        public bool DeleteUser(User user)
        {
            Delete(user);
            return true; //if no exceptions
        }
    }
}