namespace Sattelite.Entities.UserAgg
{
    using System;

    public class UserFactory
    {
        public static User Create(string userName, string displayName, string password, string email, int roleId, string createdBy)
        {
            return new User
            {
                UserName = userName,
                DisplayName = displayName,
                Password = password,
                Email = email,
                RoleId = roleId,
                CreatedDate = DateTime.Now,
                CreatedBy = createdBy
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="displayName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="roleId">default role id</param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public static User Update(int id, string userName, string displayName, string password, string email, int roleId, string createdBy)
        {
            return new User
                {
                    Id = id,
                    UserName = userName,
                    DisplayName = displayName,
                    Password = password,
                    Email = email,
                    RoleId = roleId,
                    CreatedDate = DateTime.Now,
                    CreatedBy = createdBy
                };   
        }
    }
}