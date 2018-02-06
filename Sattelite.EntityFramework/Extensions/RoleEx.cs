using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using WebMatrix.WebData;

namespace Sattelite.EntityFramework.Extensions
{
    public static class RoleEx
    {
        static SimpleRoleProvider _rolesProvider;
        static RoleEx()
        {
            _rolesProvider = (SimpleRoleProvider)Roles.Provider;
        }

        public static string[] AllRoles
        {
            get
            {
                return _rolesProvider.GetAllRoles();
            }
        }

        public static List<string> GetRoles(this ClaimsIdentity identity)
        {
            return identity.Claims
                           .Where(c => c.Type == ClaimTypes.Role)
                           .Select(c => c.Value)
                           .ToList();
        }

        public static void AddUserToRole(string userName, string role)
        {
            if (!_rolesProvider.GetRolesForUser(userName).Contains(role))
            {
                _rolesProvider.AddUsersToRoles(new[] { userName }, new[] { role });
            }
        }
    }
}
