namespace Sattelite.EntityFramework.Repository
{
    using Sattelite.Entities;
    using System.Collections.Generic;

    public interface IRoleRepository
    {
        List<Role> GetRoles();

        Role GetById(int id);

        IEnumerable<Role> GetByName(string name, int index, int numOfpage, out int numOfRecords);

        /// <summary>
        /// Create or update roles
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        bool SaveRole(Role Role);

        bool IncreaseNumOfView(int id);

        bool DeleteRole(int roleId);

        bool DeleteRole(string roleName);

        bool DeleteRole(Role role);

        Role GetRoleByName(string roleName);

        IList<CategoryPermission> GetRolePermissionsById(int roleId);
    }
}

