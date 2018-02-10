namespace Sattelite.EntityFramework.ActionResults.Admin
{
    using System;
    using System.Data;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Sattelite.Framework;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.ViewModels.Admin.Category;
    using Sattelite.EntityFramework.Repository;

    public class CategoryEditingViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
         #region variables & ctors

        private readonly ICategoryRepository _categoryRepository;
        private readonly int _categoryId;

        public CategoryEditingViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                int itemId)
            : this(viewNameExpression, itemId, DependencyResolver.Current.GetService<ICategoryRepository>())
        {
        }

        public CategoryEditingViewModelActionResult(
            Expression<Func<TController, ActionResult>> viewNameExpression, int categoryId, ICategoryRepository categoryRepository)
            : base(viewNameExpression)
        {
            _categoryId = categoryId;
            _categoryRepository = categoryRepository;
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var category = _categoryRepository.GetById(_categoryId);

            if (category == null)
                throw new NoNullAllowedException(string.Format("Category with id={0}", _categoryId).ToNotNullErrorMessage());

            var viewModel = category.MapTo<CategoryEditingViewModel>();
            GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentMustMoreThanZero(_categoryId, "CategoryId");
            Guard.ArgumentNotNull(_categoryRepository, "CategoryRepository");
        }

        #endregion
    }
}