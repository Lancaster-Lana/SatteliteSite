namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using System;
    using System.Data;
    using System.Web.Mvc;
    using Sattelite.Entities;
    using Sattelite.Entities.UserAgg;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.Repository;

    public class RoleEditingPersistence : IRoleEditingPersistence
    {
        private readonly IRoleRepository _roleRepository;

        public RoleEditingPersistence()
            : this(DependencyResolver.Current.GetService<IRoleRepository>())
        {
        }

        public RoleEditingPersistence(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public bool SaveRole(Role role)
        {
            var oldIRole = _roleRepository.GetById(role.Id);

            //var oldIRolePermissions = this._roleRepository.GetByPermissionsByRoleId(role.Id);

            if (oldIRole == null && oldIRole.Name == null)
                throw new NoNullAllowedException(string.Format("Role with id={0}", role.Id).ToNotNullErrorMessage());

            oldIRole.ModifiedDate = DateTime.Now;

            if (!oldIRole.Name.Equals(role.Name, StringComparison.InvariantCulture))
                oldIRole.Name = role.Name;

            //Update description
            oldIRole.Description = role.Description;

            //Update permissions
            oldIRole.CategoryPermissions = role.CategoryPermissions;

            return this._roleRepository.SaveRole(oldIRole);
        }
    }
}