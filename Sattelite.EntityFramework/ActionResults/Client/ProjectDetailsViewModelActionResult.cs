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

    public class ProjectDetailsViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        public const string SITE_TITLE = "Супутник НК Website - {0}";

        #region variables & ctors

        private IIdentity _userIdentity;
        private readonly int _projectId;
        private readonly int _numOfPage;

        private readonly ICategoryRepository _categoryRepository;
        private readonly IProjectRepository _projectRepository;

        public ProjectDetailsViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                IIdentity userIdentity, int projectId)
            : this(viewNameExpression, userIdentity, projectId,
                   DependencyResolver.Current.GetService<ICategoryRepository>(),
                   DependencyResolver.Current.GetService<IProjectRepository>())
        {
        }

        public ProjectDetailsViewModelActionResult(
                    Expression<Func<TController, ActionResult>> viewNameExpression,
                    IIdentity userIdentity, int projectId,
                    ICategoryRepository categoryRepository,
                    IProjectRepository projectRepository) : base(viewNameExpression)
        {
            _userIdentity = userIdentity;
            _projectId = projectId;

            _categoryRepository = categoryRepository;
            _projectRepository = projectRepository;
            _numOfPage = ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

           // _projectRepository.IncreaseNumOfView(_projectId);

            var mainViewModel = new HomePageViewModel();
            var menuViewModel = new MainMenuViewModel();
            var mainPageViewModel = new MainPageViewModel();
            var footerViewModel = new FooterViewModel();


            var categories = _categoryRepository.GetCategories();
            var subscriptions = _categoryRepository.GetUserCategories(_userIdentity.Name); //get only current user subscriptions

            if (categories != null && categories.Any())
            {
                //left menu with subscribed categories
                menuViewModel.Categories = subscriptions.ToList();

                footerViewModel.Categories = categories.ToList();
            }

            mainPageViewModel.LeftColumn = this.BindingDataForDetailsLeftColumnViewModel(_projectId);
            mainPageViewModel.RightColumn = this.BindingDataForMainPageRightColumnViewModel();

            menuViewModel.SiteTitle = string.Format(SITE_TITLE,
                ((DetailsLeftColumnViewModel)mainPageViewModel.LeftColumn).CurrentProject.ProjectContent.Name);

            mainViewModel.DashBoard = new DashboardViewModel();
            mainViewModel.MainMenu = menuViewModel;
            mainViewModel.MainPage = mainPageViewModel; //Project Content
            mainViewModel.Footer = footerViewModel;

            GetViewResult(mainViewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentMustMoreThanZero(_projectId, "ProjectId");
            Guard.ArgumentNotNull(_projectRepository, "ProjectRepository");
            Guard.ArgumentNotNull(_categoryRepository, "CategoryRepository");

            Guard.ArgumentMustMoreThanZero(_numOfPage, "NumOfPage");
        }

        #endregion

        #region private functions

        private DetailsLeftColumnViewModel BindingDataForDetailsLeftColumnViewModel(int projectId)
        {
            var project = _projectRepository.GetById(projectId);

            if (project == null)
                throw new NoNullAllowedException(string.Format("Project id={0}", projectId).ToNotNullErrorMessage());

            var viewModel = new DetailsLeftColumnViewModel();
            viewModel.CurrentProject = project;

            return viewModel;
        }

        private MainPageRightColumnViewModel BindingDataForMainPageRightColumnViewModel()
        {
            var mainPageRightCol = new MainPageRightColumnViewModel();

            //mainPageRightCol.LatestNews = _newsRepository.GetLatestNews(_numOfPage).ToList();
            //mainPageRightCol.MostViews = _newsRepository.GetMostViews(_numOfPage).ToList();

            return mainPageRightCol;
        }

        #endregion
    }
}