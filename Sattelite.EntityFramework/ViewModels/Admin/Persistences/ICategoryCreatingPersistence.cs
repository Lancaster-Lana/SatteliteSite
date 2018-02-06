
namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using Sattelite.Entities;

    public interface ICategoryCreatingPersistence
    {
        bool CreateCategory(Category category);
    }
}
