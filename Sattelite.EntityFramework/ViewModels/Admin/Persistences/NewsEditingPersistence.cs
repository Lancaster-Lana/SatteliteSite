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
            var newContent = article.NewsContent;

            if (!oldContent.Title.Equals(newContent.Title, StringComparison.InvariantCulture))
                oldArticle.NewsContent.Title = newContent.Title;

            if (!oldContent.ShortDescription.Equals(newContent.ShortDescription, StringComparison.InvariantCulture))
                oldArticle.NewsContent.ShortDescription = newContent.ShortDescription;

            if (oldContent.Content == null || !oldContent.Content.Equals(newContent.Content, StringComparison.InvariantCulture))
                oldArticle.NewsContent.Content = newContent.Content;

            //Update images
            if (newContent.SmallImage != null && !oldContent.SmallImage.Equals(newContent.SmallImage, StringComparison.InvariantCulture))
                oldArticle.NewsContent.SmallImage = newContent.SmallImage;
            if (newContent.MediumImage != null && !oldContent.MediumImage.Equals(newContent.MediumImage, StringComparison.InvariantCulture))
                oldArticle.NewsContent.MediumImage = newContent.MediumImage;
            if (newContent.BigImage != null && !oldContent.BigImage.Equals(newContent.BigImage, StringComparison.InvariantCulture))
                oldArticle.NewsContent.BigImage = newContent.BigImage;

            // Update modified tome    
            oldArticle.ModifiedDate = DateTime.Now;
            oldArticle.NewsContent.ModifiedDate = DateTime.Now;

            return _newsRepository.SaveNews(oldArticle);
        }
    }
}