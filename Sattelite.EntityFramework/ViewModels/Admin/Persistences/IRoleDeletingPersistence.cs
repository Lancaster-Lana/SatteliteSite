
namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using Sattelite.Entities.UserAgg;

    public interface IRoleDeletingPersistence
    {
        bool RemoveRole(int id);
    }
}
