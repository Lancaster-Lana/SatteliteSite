namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using System.Data;

    using Sattelite.Entities.ProjectAgg;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.Repository;

    public class CategoryDeletingPersistence : ICategoryDeletingPersistence
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryDeletingPersistence(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public bool DeleteCategory(int id)
        {
            var category = _categoryRepository.GetById(id);

            if(category == null)
                throw new NoNullAllowedException(string.Format("Category with id={0}", id).ToNotNullErrorMessage());

            return _categoryRepository.DeleteCategory(category);
        }
    }
}