namespace Sattelite.EntityFramework.ActionResults.Admin
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Sattelite.Entities.ProjectAgg;
    using Sattelite.Framework;
    using Sattelite.EntityFramework.ViewModels.Admin.Category;
    using Sattelite.EntityFramework.Repository;

    public class CategoryCreatingViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly ICategoryRepository _categoryRepository;

        public CategoryCreatingViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression) : this(viewNameExpression,
                   DependencyResolver.Current.GetService<ICategoryRepository>())
        {
        }

        public CategoryCreatingViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression, ICategoryRepository categoryRepository)
            : base(viewNameExpression)
        {
             this._categoryRepository = categoryRepository;
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var viewModel = new CategoryCreatingViewModel();
            GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            // Guard.ArgumentNotNull(this._categoryRepository, "CategoryRepository");
        }

        #endregion
    }
}