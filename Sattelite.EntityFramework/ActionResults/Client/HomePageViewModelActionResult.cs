namespace Sattelite.EntityFramework.ActionResults.Client
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Security.Principal;
    using System.Web.Mvc;
    using Sattelite.Entities.ProjectAgg;
    using Sattelite.Framework;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.ViewModels.Client;
    using Sattelite.EntityFramework.Repository;

    public class HomePageViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly ICategoryRepository _categoryRepository;
        private readonly INewsRepository _newsRepository;
        private IIdentity _userIdentity { get; set; }
        public const string APP_HEADER = "СУПУТНИК";//TODO:

        private readonly int _numOfPage;

        //public HomePageViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression, IIdentity userIdentity, string error)
        //    : this(viewNameExpression, userIdentity)
        //{

        //}

        public HomePageViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression, IIdentity userIdentity)
            : this(viewNameExpression, userIdentity,
                   DependencyResolver.Current.GetService<ICategoryRepository>(),
                   DependencyResolver.Current.GetService<INewsRepository>())
        {
        }

        public HomePageViewModelActionResult(
            Expression<Func<TController, ActionResult>> viewNameExpression, IIdentity userIdentity,
            ICategoryRepository categoryRepository,
            INewsRepository itemRepository
            ): base(viewNameExpression)
        {
            this._categoryRepository = categoryRepository;
            this._newsRepository = itemRepository;
            this._userIdentity = userIdentity; 
            this._numOfPage = this.ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var cats = this._categoryRepository.GetCategories();
            var subscribedCategories = this._categoryRepository.GetUserCategories(_userIdentity.Name).ToList();//TODO: get only current user subscriptions

            var mainViewModel = new HomePageViewModel();
            var headerViewModel = new HeaderViewModel();
            var footerViewModel = new FooterViewModel();
            var mainPageViewModel = new MainPageViewModel();

            headerViewModel.SiteTitle = APP_HEADER;
            if (cats != null && cats.Any())
            {
                headerViewModel.Categories = subscribedCategories;
                footerViewModel.Categories = cats.ToList();
            }

            mainPageViewModel.LeftColumn = this.BindingDataForMainPageLeftColumnViewModel();
            mainPageViewModel.RightColumn = this.BindingDataForMainPageRightColumnViewModel();
            
            mainViewModel.Header = headerViewModel;
            mainViewModel.DashBoard = new DashboardViewModel();
            mainViewModel.Footer = footerViewModel;
            mainViewModel.MainPage = mainPageViewModel;

            this.GetViewResult(mainViewModel).ExecuteResult(context); //context.Controller.ViewBag.ErrorMessage
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(this._categoryRepository, "CategoryRepository");
            Guard.ArgumentNotNull(this._newsRepository, "NewsRepository");
            Guard.ArgumentMustMoreThanZero(this._numOfPage, "NumOfPage");
        }

        #endregion

        #region private functions

        private MainPageRightColumnViewModel BindingDataForMainPageRightColumnViewModel()
        {
            var mainPageRightCol = new MainPageRightColumnViewModel();

            mainPageRightCol.LatestNews = this._newsRepository.GetLatestNews(this._numOfPage).ToList();
            mainPageRightCol.MostViews = this._newsRepository.GetMostViews(this._numOfPage).ToList();

            return mainPageRightCol;
        }

        private MainPageLeftColumnViewModel BindingDataForMainPageLeftColumnViewModel()
        {
            var mainPageLeftCol = new MainPageLeftColumnViewModel();

            var news = _newsRepository.GetLatestNews(_numOfPage);

            if (news != null && news.Any())
            {
                var firstItem = news.First();

                if (firstItem == null)
                    throw new NoNullAllowedException("First Item".ToNotNullErrorMessage());

                if (firstItem.NewsContent == null)
                    throw new NoNullAllowedException("First FashionBoutik".ToNotNullErrorMessage());

                mainPageLeftCol.FirstItem = firstItem;

                if (news.Count() > 1)
                {
                    mainPageLeftCol.RemainItems = news.Where(x => x.NewsContent != null && x.Id != mainPageLeftCol.FirstItem.Id).ToList();
                }
            }

            return mainPageLeftCol;
        }

        #endregion
    }
}