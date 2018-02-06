namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using Sattelite.Entities;

    public interface INewsEditingPersistence
    {
        bool SaveNews(News article);
    }
}