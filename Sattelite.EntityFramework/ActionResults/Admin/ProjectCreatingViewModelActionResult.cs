namespace Sattelite.EntityFramework.ActionResults.Admin
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Sattelite.Entities.ProjectAgg;
    using Sattelite.Entities.UserAgg;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.EntityFramework.ViewModels.Admin.Project;
    using Sattelite.Framework;

    public class ProjectCreatingViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectRoleRepository _projectRolesRepository;

        public ProjectCreatingViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression)
            : this(viewNameExpression,
            DependencyResolver.Current.GetService<ICategoryRepository>(),
            DependencyResolver.Current.GetService<IUserRepository>(),
            DependencyResolver.Current.GetService<IProjectRepository>(),
            DependencyResolver.Current.GetService<IProjectRoleRepository>())
        {
        }

        public ProjectCreatingViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression,
             ICategoryRepository categoryRepository,
             IUserRepository userRepository,
             IProjectRepository projectRepository,
             IProjectRoleRepository projectRolesRepository) : base(viewNameExpression)
        {
            _projectRepository = projectRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _projectRolesRepository = projectRolesRepository;
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var viewModel = new ProjectCreatingViewModel();

            //fill all collections (users, categories lists) to propogate dropdown controls
            //viewModel.AllCategories = _categoryRepository.GetCategories().ToList();
            //viewModel.AllUsers = _userRepository.GetUsers().ToList();
            //viewModel.AllProjectMemberRoles = _projectRolesRepository.GetProjectRoles().ToList();

            GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(_categoryRepository, "CategoryRepository");
            Guard.ArgumentNotNull(_userRepository, "UserRepository");
            Guard.ArgumentNotNull(_projectRepository, "ProjectRepository");
            Guard.ArgumentNotNull(_userRepository, "ProjectRoleRepository");
        }

        #endregion
    }
}