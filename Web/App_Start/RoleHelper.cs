using System.Web.Security;

namespace Sattelite.Utils
{
    public static class RoleHelper
    {
        /// <summary>
        /// TODO:
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllRoles()
        {
            return Roles.GetAllRoles();
        }

        public static string[] GetUserRoles(string userName)
        {
            return Roles.GetRolesForUser(userName);
        }

        public static string[] GetUsersInRole(string roleName)
        {
            return Roles.GetUsersInRole(roleName);
        }


        /// <summary>
        /// Assign role to the user
        /// </summary>
        /// <returns></returns>
        public static bool AddUserToRole (string userName, string roleName)
        {
            if (!Roles.IsUserInRole(userName, roleName))
            {
                Roles.AddUserToRole(userName, roleName);
            }

            return true;
        }

        public static bool RemoveUserFromRole(string userName, string roleName)
        {
            if (Roles.IsUserInRole(userName, roleName))
            {
                Roles.RemoveUserFromRole(userName, roleName);
            }

            return true;
        }
    }
}