namespace Sattelite.EntityFramework.ActionResults.Admin
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using Sattelite.Framework;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.EntityFramework.ViewModels.Admin.News;

    public class NewsCreatingViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly ICategoryRepository _categoryRepository;

        public NewsCreatingViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression)
            : this(viewNameExpression, DependencyResolver.Current.GetService<ICategoryRepository>())
        {
        }

        public NewsCreatingViewModelActionResult(
                    Expression<Func<TController, ActionResult>> viewNameExpression,
                    ICategoryRepository categoryRepository) : base(viewNameExpression)
        {
            _categoryRepository = categoryRepository;
        }

        #endregion

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var viewModel = new NewsCreatingViewModel();
            //viewModel.AllCategories = _categoryRepository.GetCategories().ToList(); //TODO: to be from global cach

            GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(this._categoryRepository, "CategoryRepository");
        }

    }
}