namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using System;
    using System.Data;
    using System.Web.Mvc;
    using Sattelite.Entities;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.Framework.Extensions;

    public class CategoryEditingPersistence : ICategoryEditingPersistence
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryEditingPersistence()
            : this(DependencyResolver.Current.GetService<ICategoryRepository>())
        {
        }

        public CategoryEditingPersistence(ICategoryRepository categoryeRepository)
        {
            _categoryRepository = categoryeRepository;
        }

        public bool SaveCategory(Category category)
        {
            var oldCategory = _categoryRepository.GetById(category.Id);

            if (oldCategory == null && oldCategory.Name == null)
                throw new NoNullAllowedException(string.Format("Category with id={0}", category.Id).ToNotNullErrorMessage());

            oldCategory.ModifiedDate = DateTime.Now;

            if (!oldCategory.Name.Equals(category.Name, StringComparison.InvariantCulture))
                oldCategory.Name = category.Name;

            if (!oldCategory.Description.Equals(category.Description, StringComparison.InvariantCulture))
                oldCategory.Description = category.Description;

            return _categoryRepository.SaveCategory(oldCategory);
        }

        public bool SaveCategorySubscription(string userName, int id)
        {
            return _categoryRepository.AddUserSubscription(userName, id);
        }
    }
}