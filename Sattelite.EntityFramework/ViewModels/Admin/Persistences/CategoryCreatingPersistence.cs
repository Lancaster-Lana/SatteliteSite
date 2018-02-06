namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using System;
    using System.Data;
    using System.Web.Mvc;

    using Sattelite.Entities;
    using Sattelite.Entities.ProjectAgg;
    using Sattelite.Entities.UserAgg;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.Repository;

    public class CategoryCreatingPersistence : ICategoryCreatingPersistence
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public CategoryCreatingPersistence()
            : this(DependencyResolver.Current.GetService<ICategoryRepository>(),
                    DependencyResolver.Current.GetService<IUserRepository>())
        {
        }

        public CategoryCreatingPersistence(ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public bool CreateCategory(Category category)
        {
            //Check if category with same name exists
            //var existCategory = _categoryRepository.GetByName(category.Name);
            //if (existCategory)
            //    return false;
            return _categoryRepository.SaveCategory(category);
        }
    }
}