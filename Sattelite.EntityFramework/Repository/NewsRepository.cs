using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
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
            //var news = this.GetQuery<News>()
            //                .Include(x => x.NewsContent)
            //                .ToList();

            //foreach (var item in news)
            //{
            //    item.NewsContent = this.GetQuery<NewsContent>().First(x => x.Id == item.NewsContentId);
            //}

            return news;
        }

        public IEnumerable<News> GetLatestNews(int numOfPage)
        {
            return this.GetNews().OrderByDescending(x => x.CreatedDate).Take(numOfPage);
        }

        public IEnumerable<News> GetMostViews(int numOfPage)
        {
            return this.GetNews().OrderByDescending(x => x.NewsContent.NumOfView).Take(numOfPage);
        }

        public IEnumerable<News> SeachByTitle(string titleSearchText, int index, int numOfpage, out int numOfRecords)
        {
            var items = this.GetNews();

            numOfRecords = items.Count();

            items = items.Where(x => x.NewsContent.Title.Contains(titleSearchText));

            return items.Skip((index - 1) * numOfpage).Take(numOfpage);
        }

        public IEnumerable<News> GetByCategory(int categoryId)
        {
            var items = GetNews();

            return items.Where(x => x.Category.Id == categoryId);
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
            var item = GetById(id);

            if (item != null)
            {
                item.NewsContent.NumOfView = item.NewsContent.NumOfView + 1;

                return SaveNews(item);
            }
            return false;
        }
    }
}