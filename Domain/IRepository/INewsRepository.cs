namespace Sattelite.EntityFramework.Repository
{
    using Sattelite.Entities;
    using System.Collections.Generic;

    public interface INewsRepository
    {
        News GetById(int id);

        IEnumerable<News> GetNews();

        IEnumerable<News> GetLatestNews(int numOfPage);

        IEnumerable<News> GetMostViews(int numOfPage);

        IEnumerable<News> SeachByTitle(string titleSearchText, int index, int numOfpage, out int numOfRecords);

        IEnumerable<News> GetByCategory(int categoryId);

        bool SaveNews(News news);

        bool DeleteNews(News news);

        bool IncreaseNumOfView(int id);
    }
}