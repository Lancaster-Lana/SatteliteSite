namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using System;
    using System.Data;
    using System.Web.Mvc;

    using Sattelite.Entities;
    using Sattelite.Entities.UserAgg;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.Framework.Extensions;

    public class NewsCreatingPersistence : INewsCreatingPersistence
    {
        private readonly INewsRepository _newsRepository;
        //private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public NewsCreatingPersistence()
            : this(DependencyResolver.Current.GetService<INewsRepository>(),

                   DependencyResolver.Current.GetService<IUserRepository>())
        {
        }

        public NewsCreatingPersistence(INewsRepository newsRepository, IUserRepository userRepository)
        {
            //_categoryRepository = categoryRepository;
            _newsRepository = newsRepository;
            _userRepository = userRepository;
        }

        public bool CreateNews(News news)
        {
            var user = _userRepository.GetUserByUserName(news.CreatedBy);
            if (user == null)
                throw new NoNullAllowedException("You have to login to the system to create an article !");

            news.CategoryId = news.CategoryId;
            //news.Category = category ?? throw new NoNullAllowedException("Category".ToNotNullErrorMessage());

            news.CreatedBy = news.CreatedBy;//user.UserName;
            news.CreatedDate = DateTime.Now;
            news.NewsContent.CreatedDate = DateTime.Now;
            news.NewsContent.CreatedBy = news.CreatedBy; //user.UserName;

            return _newsRepository.SaveNews(news);
        }
    }
}