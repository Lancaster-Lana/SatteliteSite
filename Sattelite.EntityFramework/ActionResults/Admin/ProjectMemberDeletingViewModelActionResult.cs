namespace Sattelite.EntityFramework.ActionResults.Admin
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using Sattelite.Framework;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.ViewModels.Admin.Project;
    using Sattelite.EntityFramework.Repository;

    public class ProjectMemberDeletingViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        //private readonly int _projectId;
        private readonly int _projectMemberId;
        private readonly IProjectRepository _projectRepository;

        public ProjectMemberDeletingViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                int projectMemberId)
            : this(viewNameExpression, 
                  projectMemberId,
                  DependencyResolver.Current.GetService<IProjectRepository>())
        {
        }

        public ProjectMemberDeletingViewModelActionResult(
            Expression<Func<TController, ActionResult>> viewNameExpression, 
            int projectMemberId,
            IProjectRepository projectRepository)
            : base(viewNameExpression)
        {
            //_projectId = projectId;
            _projectMemberId = projectMemberId;
            _projectRepository = projectRepository;
        }

        #endregion

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var projectMember = _projectRepository.GetProjectMemberById(_projectMemberId);

            if (projectMember == null)
                throw new NoNullAllowedException(string.Format("projectMember with id={0} cannot be deleted", _projectMemberId).ToNotNullErrorMessage());

            var viewModel = projectMember.MapTo<ProjectMemberViewModel>();

            //Show confirmation dialog with Delete button
            GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentMustMoreThanZero(_projectMemberId, "ProjectMemberId");
            Guard.ArgumentNotNull(_projectRepository, "ProjectRepository");
        }
    }
}