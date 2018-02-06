
namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using Sattelite.Entities;

    public interface IRoleEditingPersistence
    {
        bool SaveRole(Role role);
    }
}
