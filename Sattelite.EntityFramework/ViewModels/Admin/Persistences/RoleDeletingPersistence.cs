namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using System.Data;
    using Sattelite.Entities.UserAgg;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.Framework.Extensions;
 
    /// <summary>
    /// 
    /// </summary>
    public class RoleDeletingPersistence : IRoleDeletingPersistence
    {
        private readonly IRoleRepository _roleRepository;

        public RoleDeletingPersistence(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        public bool RemoveRole(int id)
        {
            var role = this._roleRepository.GetById(id);

            if(role == null)
                throw new NoNullAllowedException(string.Format("Item with id={0}", id).ToNotNullErrorMessage());

            return this._roleRepository.DeleteRole(role);
        }
    }
}