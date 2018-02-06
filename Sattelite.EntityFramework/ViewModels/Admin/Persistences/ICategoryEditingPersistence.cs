namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using Sattelite.Entities;

    public interface ICategoryEditingPersistence
    {
        bool SaveCategory(Category category);
        bool SaveCategorySubscription(string userName, int id);
    }
}
