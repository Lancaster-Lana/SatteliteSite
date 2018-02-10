namespace Sattelite.EntityFramework.ActionResults.Client
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Security.Principal;
    using System.Web.Mvc;

    using Sattelite.Framework;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.ViewModels.Client;
    using Sattelite.EntityFramework.Repository;

    public class HomePageViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        public const string APP_HEADER = "IT-SATTELITE";

        private IIdentity _userIdentity;

        private readonly ICategoryRepository _categoryRepository;
        private readonly INewsRepository _newsRepository;
        private readonly int _numOfPage;

        //public HomePageViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression, IIdentity userIdentity, string error)
        //    : this(viewNameExpression, userIdentity)
        //{

        //}

        public HomePageViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression,
                  IIdentity userIdentity)
            : this(viewNameExpression, userIdentity,
                   DependencyResolver.Current.GetService<ICategoryRepository>(),
                   DependencyResolver.Current.GetService<INewsRepository>())
        {
        }

        public HomePageViewModelActionResult(
            Expression<Func<TController, ActionResult>> viewNameExpression, IIdentity userIdentity,
            ICategoryRepository categoryRepository,
            INewsRepository itemRepository
            ) : base(viewNameExpression)
        {
            _userIdentity = userIdentity;
            _categoryRepository = categoryRepository;
            _newsRepository = itemRepository;
            _numOfPage = ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);
            var userName = _userIdentity.Name;
            var categories = _categoryRepository.GetCategories();
            var subscribedCategories = _categoryRepository.GetUserCategories(userName).ToList();//TODO: get only current user subscriptions

            var mainViewModel = new HomePageViewModel();
            var menuViewModel = new MainMenuViewModel();
            var footerViewModel = new FooterViewModel();
            var mainPageViewModel = new MainPageViewModel();

            menuViewModel.SiteTitle = APP_HEADER;
            if (categories != null && categories.Any())
            {
                menuViewModel.Categories = subscribedCategories;
                footerViewModel.Categories = categories.ToList();
            }

            mainPageViewModel.LeftColumn = this.BindingDataForMainPageLeftColumnViewModel();
            mainPageViewModel.RightColumn = this.BindingDataForMainPageRightColumnViewModel();

            mainViewModel.MainMenu = menuViewModel;
            mainViewModel.MainPage = mainPageViewModel;
            mainViewModel.DashBoard = new DashboardViewModel();
            mainViewModel.Footer = footerViewModel;

            GetViewResult(mainViewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(_categoryRepository, "CategoryRepository");
            Guard.ArgumentNotNull(_newsRepository, "NewsRepository");
            Guard.ArgumentMustMoreThanZero(_numOfPage, "NumOfPage");
        }

        #endregion

        #region private functions

        private MainPageRightColumnViewModel BindingDataForMainPageRightColumnViewModel()
        {
            var mainPageRightCol = new MainPageRightColumnViewModel();

            mainPageRightCol.LatestNews = _newsRepository.GetLatestNews(_numOfPage).ToList();
            mainPageRightCol.MostViews = _newsRepository.GetMostViews(_numOfPage).ToList();

            return mainPageRightCol;
        }

        private MainPageLeftColumnViewModel BindingDataForMainPageLeftColumnViewModel()
        {
            var mainPageLeftCol = new MainPageLeftColumnViewModel();

            var news = _newsRepository.GetLatestNews(_numOfPage);

            if (news != null && news.Any())
            {
                //NOTE: don't need to show first article any more
                /* 
                var firstArticle = news.First();

                if (firstArticle == null)
                    throw new NoNullAllowedException("First Article".ToNotNullErrorMessage());
                if (firstArticle.NewsContent == null)
                    throw new NoNullAllowedException("First Article Content".ToNotNullErrorMessage());
                mainPageLeftCol.FirstArticle = firstArticle;

                if (news.Count() > 1)
                {
                    mainPageLeftCol.RemainNews = news.Where(x => x.NewsContent != null 
                                                         && x.Id != mainPageLeftCol.FirstArticle.Id).ToList();
                }*/
            }
            return mainPageLeftCol;
        }

        #endregion
    }
}