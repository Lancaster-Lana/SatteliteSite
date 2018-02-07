
namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using Sattelite.Entities;

    public interface ICategoryCreatingPersistence
    {
        /// <summary>
        /// Chack if name used already
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        bool Validate(Category category);

        bool CreateCategory(Category category);
    }
}
