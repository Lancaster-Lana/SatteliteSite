namespace Sattelite.EntityFramework.ActionResults.Admin
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.EntityFramework.ViewModels.Admin.Project;
    using Sattelite.EntityFramework.ViewModels.Admin.Role;
    using Sattelite.Framework;

    public class ProjectRoleCreatingViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly IProjectRoleRepository _projectRolesRepository;

        public ProjectRoleCreatingViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression)
            : this(viewNameExpression,
            DependencyResolver.Current.GetService<IProjectRoleRepository>())
        {
        }

        public ProjectRoleCreatingViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression,
             IProjectRoleRepository projectRolesRepository) : base(viewNameExpression)
        {
            _projectRolesRepository = projectRolesRepository;
        }

        #endregion

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var viewModel = new ProjectRoleViewModel(); //ProjectRoleCreatingViewModel
            GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(_projectRolesRepository, "ProjectRoleRepository");
        }
    }
}