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

        private readonly IProjectRepository _projectRepository;
 
        public ProjectCreatingViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression)
            : this(viewNameExpression, DependencyResolver.Current.GetService<IProjectRepository>())
        {
        }

        public ProjectCreatingViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression,
             IProjectRepository projectRepository) : base(viewNameExpression)
        {
            _projectRepository = projectRepository;
        }

        /*public ProjectCreatingViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression,
             ICategoryRepository categoryRepository,
             IUserRepository userRepository,
             IProjectRepository projectRepository,
             IProjectRoleRepository projectRolesRepository) : base(viewNameExpression)
        {
            _projectRepository = projectRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _projectRolesRepository = projectRolesRepository;
        }*/

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var viewModel = new ProjectCreatingViewModel();
            GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(_projectRepository, "ProjectRepository");
        }

        #endregion
    }
}