namespace Sattelite.EntityFramework.ActionResults.Admin
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using Sattelite.Framework;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.ViewModels;
    using Sattelite.EntityFramework.ViewModels.Admin.Project;
    using Sattelite.EntityFramework.Repository;

    public class ProjectsViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectRoleRepository _projectRoleRepository;

        private readonly string _titleSearchText;
        private readonly int _categorySearchId;
        private readonly int _currentPage;
        private readonly int _numOfPage;

        public ProjectsViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                string titleSearchText,
                int categoryId,
                int currentPage)
            : this(viewNameExpression, titleSearchText, categoryId, currentPage,

                   DependencyResolver.Current.GetService<ICategoryRepository>(),
                   DependencyResolver.Current.GetService<IUserRepository>(),
                   DependencyResolver.Current.GetService<IProjectRepository>(),
                   DependencyResolver.Current.GetService<IProjectRoleRepository>()
            )
        {
        }

        public ProjectsViewModelActionResult(
                    Expression<Func<TController, ActionResult>> viewNameExpression,
                    string titleSearchText, int categorySearchId,
                    int currentPage,
                    ICategoryRepository categoryRepository,
                    IUserRepository userRepository,
                    IProjectRepository projectRepository,
                    IProjectRoleRepository projectRoleRepository
                    )
            : base(viewNameExpression)
        {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _projectRoleRepository = projectRoleRepository;

            _categorySearchId = categorySearchId;
            _titleSearchText = titleSearchText;
            _currentPage = currentPage;
            _numOfPage = ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
        }

        #endregion

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            int numOfRecords;

            var viewModel = new ProjectListViewModel
            {
                //Filtered projects with search criteria
                Projects = _projectRepository.SeachByNameAndCategory(
                        _titleSearchText, _categorySearchId,
                        _currentPage, _numOfPage, out numOfRecords).ToList(),
                PagingData = new PagingViewModel(_currentPage, _numOfPage, numOfRecords,
                     string.Format("{0}", "/Admin/Project/Index/{page}"), null)
            };
            GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(_categoryRepository, "CategoryRepository");
            Guard.ArgumentNotNull(_userRepository, "UserRepository");
            Guard.ArgumentNotNull(_projectRepository, "ProjectRepository");
            Guard.ArgumentNotNull(_projectRoleRepository, "ProjectRoleRepository");
            Guard.ArgumentMustMoreThanZero(_numOfPage, "NumOfPage");
            //Guard.ArgumentMustMoreThanZero(_currentPage, "CurrentPage");
        }
    }
}