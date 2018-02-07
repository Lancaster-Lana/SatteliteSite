namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using System;
    using System.Web.Mvc;

    using Sattelite.Entities;
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

        public bool Validate(Category category)
        {
            //Check if there exists category with same name 
            var existCategory = _categoryRepository.GetByName(category.Name) != null;
            return !existCategory;
        }

        public bool CreateCategory(Category category)
        {
            return _categoryRepository.SaveCategory(category);
        }
    }
}