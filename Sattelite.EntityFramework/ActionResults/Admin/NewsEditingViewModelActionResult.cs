namespace Sattelite.EntityFramework.ActionResults.Admin
{
    using System;
    using System.Data;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using Sattelite.Framework;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.EntityFramework.ViewModels.Admin.News;

    public class NewsEditingViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly int _newsId;
        private readonly INewsRepository _newsRepository;
        private readonly ICategoryRepository _categoryRepository;

        public NewsEditingViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                int newsId)
            : this(viewNameExpression, newsId, 
                  DependencyResolver.Current.GetService<INewsRepository>(),
                  DependencyResolver.Current.GetService<ICategoryRepository>())
        {
        }

        public NewsEditingViewModelActionResult(
            Expression<Func<TController, ActionResult>> viewNameExpression,
            int newsId, INewsRepository newsRepository,
            ICategoryRepository categoryRepository)
            : base(viewNameExpression)
        {
            _newsId = newsId;
            _newsRepository = newsRepository;
            _categoryRepository = categoryRepository;
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var article = _newsRepository.GetById(_newsId);

            if (article == null)
                throw new NoNullAllowedException(string.Format("Article\\News with id={0}", this._newsId).ToNotNullErrorMessage());

            var viewModel = article.MapTo<NewsEditingViewModel>();
            //viewModel.AllCategories = _categoryRepository.GetCategories().ToList();

            GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(_categoryRepository, "CategoryRepository");
            Guard.ArgumentNotNull(_newsRepository, "NewsRepository");
            Guard.ArgumentMustMoreThanZero(_newsId, "NewsId");
        }

        #endregion
    }
}