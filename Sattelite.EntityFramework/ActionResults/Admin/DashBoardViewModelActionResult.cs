namespace Sattelite.EntityFramework.ActionResults.Admin
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Sattelite.Framework;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.ViewModels;
    using Sattelite.EntityFramework.ViewModels.Admin.DashBoard;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.Entities.UserAgg;
    using Sattelite.Entities.ProjectAgg;


    /// <summary>
    /// Admin Dashboard
    /// </summary>
    public class DashBoardViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly string _titleSearchText;
        private readonly int _currentPage;
        private readonly int _numOfPage;

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        private readonly ICategoryRepository _categoryRepository;
        private readonly IProjectRoleRepository _projectRoleRepository;
        private readonly INewsRepository _newsRepository;

        public DashBoardViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                string titleSearchText,
                int currentPage)
            : this(viewNameExpression, titleSearchText, currentPage,
                  DependencyResolver.Current.GetService<IUserRepository>(),
                  DependencyResolver.Current.GetService<IRoleRepository>(),
                  DependencyResolver.Current.GetService<ICategoryRepository>(),
                  DependencyResolver.Current.GetService<IProjectRoleRepository>(),
                  DependencyResolver.Current.GetService<INewsRepository>())
        {
        }

        public DashBoardViewModelActionResult(
                    Expression<Func<TController, ActionResult>> viewNameExpression,
                    string titleSearchText,
                    int currentPage,
                    IUserRepository userRepository,
                    IRoleRepository roleRepository,
                    ICategoryRepository categoryRepository,
                    IProjectRoleRepository projectRoleRepository,
                    INewsRepository newsRepository) : base(viewNameExpression)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _categoryRepository = categoryRepository;
            _projectRoleRepository = projectRoleRepository;
            _newsRepository = newsRepository;

            _titleSearchText = titleSearchText;
            _currentPage = currentPage;
            _numOfPage = ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);
            int numOfRecords;
            var dashBoardViewModel = new DashBoardViewModel
            {
                //Init all collections for admin board
                AllUsers = _userRepository.GetUsers().ToList(),
                AllRoles = _roleRepository.GetRoles().ToList(),
                AllCategories = _categoryRepository.GetCategories().ToList(),
                AllProjectRoles = _projectRoleRepository.GetProjectRoles().ToList(),
                AllNews = _newsRepository.SeachByTitle(_titleSearchText, _currentPage, _numOfPage, out numOfRecords)?.ToList(),

                PagingData = new PagingViewModel(_currentPage, _numOfPage, numOfRecords,
                                                    string.Format("{0}","/Admin/DashBoard/Index/{page}"), null)
            };

            GetViewResult(dashBoardViewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(_newsRepository, "NewsRepository");
            Guard.ArgumentNotNull(_newsRepository, "NewsRepository");
            Guard.ArgumentNotNull(_newsRepository, "NewsRepository");
            Guard.ArgumentNotNull(_newsRepository, "NewsRepository");
            Guard.ArgumentNotNull(_newsRepository, "NewsRepository");

            Guard.ArgumentMustMoreThanZero(_currentPage, "CurrentPage");
            Guard.ArgumentMustMoreThanZero(_numOfPage, "NumOfPage");
        }

        #endregion
    }
}