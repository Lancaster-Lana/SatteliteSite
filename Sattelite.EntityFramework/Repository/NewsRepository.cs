using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity;
using Sattelite.Entities;

namespace Sattelite.EntityFramework.Repository
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        public NewsRepository()
           : this(DependencyResolver.Current.GetService<SatteliteDBContext>())
        {
        }

        public NewsRepository(SatteliteDBContext context) : base(context)
        {
        }

        public News GetById(int id)
        {
            return GetOne(x => x.Id == id);
        }

        public IEnumerable<News> GetNews()
        {
            var news = GetIncluding(x => x.CreatedDate.HasValue,
                             s => s.NewsContent);
            return news;
        }

        public IEnumerable<News> GetLatestNews(int numOfPage)
        {
            return GetNews().OrderByDescending(x => x.CreatedDate).Take(numOfPage);
        }

        public IEnumerable<News> GetMostViews(int numOfPage)
        {
            return GetNews().OrderByDescending(x => x.NewsContent.NumOfView).Take(numOfPage);
        }

        public IEnumerable<News> SeachByTitle(string titleSearchText, int index, int numOfpage, out int numOfRecords)
        {
            var articles = GetNews();

            numOfRecords = articles.Count();

            articles = articles.Where(x => x.NewsContent.Title.Contains(titleSearchText));

            return articles.Skip((index - 1) * numOfpage).Take(numOfpage);
        }

        public IEnumerable<News> GetByCategory(int categoryId)
        {
            var articles = GetNews();

            return articles.Where(x => x.Category.Id == categoryId);
        }

        public bool SaveNews(News article)
        {
            Save(article);
            return true;
        }

        public bool DeleteNews(News article)
        {
            Delete(article);
            return true;
        }

        public bool IncreaseNumOfView(int id)
        {
            var article = GetById(id);

            if (article != null && article.NewsContent != null)
            {
                article.NewsContent.NumOfView = article.NewsContent.NumOfView + 1;
                return SaveNews(article);
            }
            return false;
        }
    }
}