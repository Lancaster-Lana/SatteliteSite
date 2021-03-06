﻿namespace Sattelite.EntityFramework.ActionResults.Client
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

    public class CategoryViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private IIdentity _userIdentity;
        private readonly int _categoryId;
        private readonly int _numOfPage;

        private readonly ICategoryRepository _categoryRepository;
        private readonly INewsRepository _newsRepository;
        private readonly IProjectRepository _projectRepository;

        public CategoryViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                IIdentity userIdentity,
                int categoryId)
            : this(viewNameExpression, userIdentity, categoryId,
                   DependencyResolver.Current.GetService<ICategoryRepository>(),
                   DependencyResolver.Current.GetService<INewsRepository>(),
                   DependencyResolver.Current.GetService<IProjectRepository>())
        {
        }

        public CategoryViewModelActionResult(
                    Expression<Func<TController, ActionResult>> viewNameExpression,
                    IIdentity userIdentity,
                    int categoryId, ICategoryRepository categoryRepository,
                    INewsRepository newsRepository,
                    IProjectRepository projectRepository)
            : base(viewNameExpression)
        {
            _categoryId = categoryId;
            _categoryRepository = categoryRepository;

            _newsRepository = newsRepository;
            _projectRepository = projectRepository;

            _userIdentity = userIdentity;
            _numOfPage = ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var categories = _categoryRepository.GetCategories();
            var subscriptions = _categoryRepository.GetUserCategories(_userIdentity.Name);//TODO: get only current user subscriptions

            var mainViewModel = new HomePageViewModel();
            var menuViewModel = new MainMenuViewModel();
            var footerViewModel = new FooterViewModel();
            var mainPageViewModel = new MainPageViewModel();

            if (categories != null && categories.Any())
            {
                menuViewModel.Categories = subscriptions.ToList();
                footerViewModel.Categories = categories.ToList();
            }

            mainPageViewModel.LeftColumn = BindCategoryData_LeftColumnViewModel(_categoryId);
            mainPageViewModel.RightColumn = BindDataForMainPage_RightColumnViewModel();

            var news = ((CategoryLeftColumnViewModel)mainPageViewModel.LeftColumn).News;
            menuViewModel.SiteTitle = (news != null && news.Count > 0) 
                ? string.Format("{0}", news.FirstOrDefault().Category.Name)
                :"Супутник НК Website";

            mainViewModel.MainMenu = menuViewModel;
            mainViewModel.DashBoard = new DashboardViewModel();
            mainViewModel.Footer = footerViewModel;
            mainViewModel.MainPage = mainPageViewModel;

            GetViewResult(mainViewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentMustMoreThanZero(_categoryId, "CategoryId");
            Guard.ArgumentNotNull(_categoryRepository, "CategoryRepository");
            Guard.ArgumentNotNull(_newsRepository, "NewsRepository");
            Guard.ArgumentMustMoreThanZero(_numOfPage, "NumOfPage");
        }

        #endregion

        #region private functions

        private CategoryLeftColumnViewModel BindCategoryData_LeftColumnViewModel(int categoryId)
        {
            var viewModel = new CategoryLeftColumnViewModel();

            var news = _newsRepository.GetByCategory(categoryId);
            if (news == null)
                throw new NoNullAllowedException("News".ToNotNullErrorMessage());

            if (news.Any())
            {
                viewModel.News = news.ToList();
            }

            var projects = _projectRepository.GetByCategory(categoryId);
            if (projects.Any())
            {
                viewModel.Projects = projects.ToList();
            }

            return viewModel;
        }

        private MainPageRightColumnViewModel BindDataForMainPage_RightColumnViewModel()
        {
            var mainPageRightCol = new MainPageRightColumnViewModel();

            mainPageRightCol.LatestNews = _newsRepository.GetLatestNews(_numOfPage).ToList();
            mainPageRightCol.MostViews = _newsRepository.GetMostViews(_numOfPage).ToList();
            
            //TODO:
            mainPageRightCol.Projects = _projectRepository.GetProjects(_numOfPage).ToList();
            //mainPageRightCol.Categories = _categoriesRepository.GetCategories().ToList();

            return mainPageRightCol;
        }

        #endregion
    }
}