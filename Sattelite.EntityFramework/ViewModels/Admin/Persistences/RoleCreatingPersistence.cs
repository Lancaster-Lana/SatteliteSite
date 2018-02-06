namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using System.Web.Mvc;
    using Sattelite.Entities;
    using Sattelite.EntityFramework.Repository;

    public class RoleCreatingPersistence : IRoleCreatingPersistence
    {
        private readonly IRoleRepository _roleRepository;
        //private readonly IUserRepository _userRepository;

        public RoleCreatingPersistence()
            : this(DependencyResolver.Current.GetService<IRoleRepository>())
        {
        }

        public RoleCreatingPersistence(IRoleRepository roleRepository/*, IUserRepository userRepository*/)
        {
            _roleRepository = roleRepository;
        }

        public bool CreateRole(Role role) //TODO: list of permissions
        {
            return _roleRepository.SaveRole(role);
        }
    }
}