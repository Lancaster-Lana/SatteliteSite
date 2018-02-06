namespace Sattelite.EntityFramework.ActionResults.Client
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using Sattelite.Framework;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.ViewModels.Client;
    using Sattelite.EntityFramework.Repository;

    public class DetailsViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly ICategoryRepository _categoryRepository;
        private readonly INewsRepository _newsRepository;
        private readonly int _articleId;

        private readonly int _numOfPage;

        public DetailsViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                int articleId)
            : this(viewNameExpression, articleId,
                   DependencyResolver.Current.GetService<ICategoryRepository>(),
                   DependencyResolver.Current.GetService<INewsRepository>())
        {
        }

        public DetailsViewModelActionResult(
                    Expression<Func<TController, ActionResult>> viewNameExpression,
                    int articleId,
                    ICategoryRepository categoryRepository, 
                    INewsRepository itemRepository) : base(viewNameExpression)
        {
            _categoryRepository = categoryRepository;
            _newsRepository = itemRepository;
            _articleId = articleId;

            _numOfPage = ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

           _newsRepository.IncreaseNumOfView(_articleId);

            var mainViewModel = new HomePageViewModel();
            var headerViewModel = new HeaderViewModel();
            var footerViewModel = new FooterViewModel();
            var mainPageViewModel = new MainPageViewModel();

            var categories = _categoryRepository.GetCategories();
            if (categories != null && categories.Any())
            {
                headerViewModel.Categories = categories.ToList();
                footerViewModel.Categories = categories.ToList();
            }

            mainPageViewModel.LeftColumn = this.BindingDataForDetailsLeftColumnViewModel(_articleId);
            mainPageViewModel.RightColumn = this.BindingDataForMainPageRightColumnViewModel();

            headerViewModel.SiteTitle = string.Format("Супутник НК Website - {0}",
                ((DetailsLeftColumnViewModel)mainPageViewModel.LeftColumn).CurrentItem.NewsContent.Title);

            mainViewModel.Header = headerViewModel;
            mainViewModel.DashBoard = new DashboardViewModel();
            mainViewModel.Footer = footerViewModel;
            mainViewModel.MainPage = mainPageViewModel;

            GetViewResult(mainViewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(_categoryRepository, "CategoryRepository");
            Guard.ArgumentNotNull(_newsRepository, "NewsRepository");
            Guard.ArgumentMustMoreThanZero(_numOfPage, "NumOfPage");
            Guard.ArgumentMustMoreThanZero(_articleId, "NewsId");
        }

        #endregion

        #region private functions

        private DetailsLeftColumnViewModel BindingDataForDetailsLeftColumnViewModel(int itemId)
        {
            var viewModel = new DetailsLeftColumnViewModel();

            var item = this._newsRepository.GetById(itemId);

            if (item == null)
                throw new NoNullAllowedException(string.Format("Item id={0}", itemId).ToNotNullErrorMessage());

            viewModel.CurrentItem = item;

            return viewModel;
        }

        private MainPageRightColumnViewModel BindingDataForMainPageRightColumnViewModel()
        {
            var mainPageRightCol = new MainPageRightColumnViewModel();

            mainPageRightCol.LatestNews = this._newsRepository.GetLatestNews(this._numOfPage).ToList();
            mainPageRightCol.MostViews = this._newsRepository.GetMostViews(this._numOfPage).ToList();

            return mainPageRightCol;
        }

        #endregion
    }
}