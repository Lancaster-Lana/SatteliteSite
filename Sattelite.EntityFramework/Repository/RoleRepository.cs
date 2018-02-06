namespace Sattelite.EntityFramework.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Sattelite.Entities;

    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(SatteliteDBContext context)
            : base(context)
        {
        }

        public List<Role> GetRoles()
        {
            return GetQuery<Role>().ToList();
        }

        public IEnumerable<Role> GetByName(string name, int index, int numOfpage, out int numOfRecords)
        {
            var items = GetRoles();

            numOfRecords = items.Count();

            items = items.Where(x => x.Name.Contains(name)).ToList();

            return items.Skip((index - 1) * numOfpage).Take(numOfpage);
        }

        public Role GetById(int id)
        {
            return GetOne(x => x.Id == id);
        }

        public Role GetRoleByName(string roleName)
        {
            return GetOne(x => x.Name.Equals(roleName, System.StringComparison.OrdinalIgnoreCase));
        }

        public IList<CategoryPermission> GetRolePermissionsById(int roleId)
        {
            return GetById(roleId).CategoryPermissions?.ToList(); //TODO: DB
        }

        public bool SaveRole(Role role)
        {
            Save(role);
            return true;
        }

        public bool IncreaseNumOfView(int id)
        {
            var role = GetById(id);

            if (role != null)
            {
                return SaveRole(role);
            }

            return false;
        }
        public bool DeleteRole(Role role)
        {
            Delete(role);
            return true;
        }

        public bool DeleteRole(int roleId)
        {
            var role = GetById(roleId);
            return DeleteRole(role);
        }

        public bool DeleteRole(string roleName)
        {
            var role = GetRoleByName(roleName);
            return DeleteRole(role);
        }
    }
}