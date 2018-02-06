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
            this._categoryRepository = categoryRepository;
            this._userRepository = userRepository;
        }

        public bool CreateCategory(Category category)
        {
            return this._categoryRepository.SaveCategory(category);
        }
    }
}