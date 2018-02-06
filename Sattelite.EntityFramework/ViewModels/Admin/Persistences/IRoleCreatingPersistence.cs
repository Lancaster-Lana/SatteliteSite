
namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using Sattelite.Entities;

    public interface IRoleCreatingPersistence
    {
        bool CreateRole(Role role);
    }
}
