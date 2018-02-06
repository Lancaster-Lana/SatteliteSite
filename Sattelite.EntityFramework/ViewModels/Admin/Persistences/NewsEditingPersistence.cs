namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using System;
    using System.Data;
    using System.Web.Mvc;

    using Sattelite.Entities;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.Repository;

    public class NewsEditingPersistence : INewsEditingPersistence
    {
        private readonly INewsRepository _newsRepository;
        private readonly ICategoryRepository _categoryRepository;

        public NewsEditingPersistence()
            : this(DependencyResolver.Current.GetService<INewsRepository>(),
                    DependencyResolver.Current.GetService<ICategoryRepository>())
        {
        }

        public NewsEditingPersistence(INewsRepository itemRepository, ICategoryRepository categoryRepository)
        {
            _newsRepository = itemRepository;
            _categoryRepository = categoryRepository;
        }

        public bool SaveNews(News article)
        {
            //var category = _categoryRepository.GetById(article.CategoryId);

            //if (category == null)
            //    throw new NoNullAllowedException("Category".ToNotNullErrorMessage());

            var oldArticle = _newsRepository.GetById(article.Id);

            if (oldArticle == null && oldArticle.NewsContent == null)
                throw new NoNullAllowedException(string.Format("Item with id={0}", article.Id).ToNotNullErrorMessage());

            //if(category.Id != oldArticle.Category.Id)
            //    oldArticle.Category = category;

            oldArticle.CategoryId = article.CategoryId;

            // Mapping details
            var oldContent = oldArticle.NewsContent;
            var newsContent = article.NewsContent;

            if (!oldContent.Title.Equals(newsContent.Title, StringComparison.InvariantCulture))
                oldArticle.NewsContent.Title = newsContent.Title;

            if (!oldContent.ShortDescription.Equals(newsContent.ShortDescription, StringComparison.InvariantCulture))
                oldArticle.NewsContent.ShortDescription = newsContent.ShortDescription;

            if (oldContent.Content == null || !oldContent.Content.Equals(newsContent.Content, StringComparison.InvariantCulture))
                oldArticle.NewsContent.Content = newsContent.Content;

            //Update images
            if (newsContent.SmallImage != null && !oldContent.SmallImage.Equals(newsContent.SmallImage, StringComparison.InvariantCulture))
                oldArticle.NewsContent.SmallImage = newsContent.SmallImage;
            if (newsContent.MediumImage != null && !oldContent.MediumImage.Equals(newsContent.MediumImage, StringComparison.InvariantCulture))
                oldArticle.NewsContent.MediumImage = newsContent.MediumImage;
            if (newsContent.BigImage != null && !oldContent.BigImage.Equals(newsContent.BigImage, StringComparison.InvariantCulture))
                oldArticle.NewsContent.BigImage = newsContent.BigImage;

            // Update modified tome    
            oldArticle.ModifiedDate = DateTime.Now;
            oldArticle.NewsContent.ModifiedDate = DateTime.Now;

            return _newsRepository.SaveNews(oldArticle);
        }
    }
}