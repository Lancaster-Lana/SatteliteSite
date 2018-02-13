namespace Sattelite.EntityFramework.ActionResults.Client
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Security.Principal;

    using Sattelite.Framework;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.ViewModels.Client;
    using Sattelite.EntityFramework.Repository;

    public class NewsDetailsViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        public const string SITE_TITLE = "Супутник НК Website - {0}";

        #region variables & ctors

        private IIdentity _userIdentity;
        private readonly int _articleId;
        private readonly int _numOfPage;

        private readonly ICategoryRepository _categoryRepository;
        private readonly INewsRepository _newsRepository;

        public NewsDetailsViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                IIdentity userIdentity, int articleId)
            : this(viewNameExpression, userIdentity, articleId,
                   DependencyResolver.Current.GetService<ICategoryRepository>(),
                   DependencyResolver.Current.GetService<INewsRepository>())
        {
        }

        public NewsDetailsViewModelActionResult(
                    Expression<Func<TController, ActionResult>> viewNameExpression,
                    IIdentity userIdentity, int articleId,
                    ICategoryRepository categoryRepository,
                    INewsRepository itemRepository) : base(viewNameExpression)
        {
            _userIdentity = userIdentity;
            _articleId = articleId;

            _categoryRepository = categoryRepository;
            _newsRepository = itemRepository;
            _numOfPage = ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            _newsRepository.IncreaseNumOfView(_articleId);

            var mainViewModel = new HomePageViewModel();
            var menuViewModel = new MainMenuViewModel();
            var footerViewModel = new FooterViewModel();
            var mainPageViewModel = new MainPageViewModel();

            var categories = _categoryRepository.GetCategories();
            var subscriptions = _categoryRepository.GetUserCategories(_userIdentity.Name); //get only current user subscriptions

            if (categories != null && categories.Any())
            {
                //left menu with subscribed categories
                menuViewModel.Categories = subscriptions.ToList();

                footerViewModel.Categories = categories.ToList();
            }

            mainPageViewModel.LeftColumn = this.BindingDataForDetailsLeftColumnViewModel(_articleId);
            mainPageViewModel.RightColumn = this.BindingDataForMainPageRightColumnViewModel();

            menuViewModel.SiteTitle = string.Format(SITE_TITLE,
                ((DetailsLeftColumnViewModel)mainPageViewModel.LeftColumn).CurrentArticle.NewsContent.Title);

            mainViewModel.DashBoard = new DashboardViewModel();
            mainViewModel.MainMenu = menuViewModel;
            mainViewModel.MainPage = mainPageViewModel; //article content
            mainViewModel.Footer = footerViewModel;

            GetViewResult(mainViewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentMustMoreThanZero(_articleId, "NewsId");
            Guard.ArgumentNotNull(_newsRepository, "NewsRepository");
            Guard.ArgumentNotNull(_categoryRepository, "CategoryRepository");

            Guard.ArgumentMustMoreThanZero(_numOfPage, "NumOfPage");
        }

        #endregion

        #region private functions

        private DetailsLeftColumnViewModel BindingDataForDetailsLeftColumnViewModel(int articleId)
        {
            var article = _newsRepository.GetById(articleId);

            if (article == null)
                throw new NoNullAllowedException(string.Format("Article id={0}", articleId).ToNotNullErrorMessage());

            var viewModel = new DetailsLeftColumnViewModel();
            viewModel.CurrentArticle = article;

            return viewModel;
        }

        private MainPageRightColumnViewModel BindingDataForMainPageRightColumnViewModel()
        {
            var mainPageRightCol = new MainPageRightColumnViewModel();

            mainPageRightCol.LatestNews = _newsRepository.GetLatestNews(_numOfPage).ToList();
            mainPageRightCol.MostViews = _newsRepository.GetMostViews(_numOfPage).ToList();

            return mainPageRightCol;
        }

        #endregion
    }
}