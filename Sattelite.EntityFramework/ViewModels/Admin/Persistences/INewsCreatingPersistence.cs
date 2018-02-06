
namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using Sattelite.Entities;

    public interface INewsCreatingPersistence
    {
        bool CreateNews(News article);
    }
}