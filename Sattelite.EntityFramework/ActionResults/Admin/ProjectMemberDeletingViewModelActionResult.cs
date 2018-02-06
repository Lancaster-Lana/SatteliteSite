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

        private readonly int _projectId;
        private readonly int _projectMemberId;
        private readonly IProjectRepository _projectRepository;

        public ProjectMemberDeletingViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                int projectId, int projectMemeberId)
            : this(viewNameExpression, 
                  projectId, projectMemeberId,
                  DependencyResolver.Current.GetService<IProjectRepository>())
        {
        }

        public ProjectMemberDeletingViewModelActionResult(
            Expression<Func<TController, ActionResult>> viewNameExpression, 
            int projectId, int projectMemberId,
            IProjectRepository projectRepository)
            : base(viewNameExpression)
        {
            _projectId = projectId;
            _projectMemberId = projectMemberId;
            _projectRepository = projectRepository;
        }

        #endregion

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var project = _projectRepository.GetById(_projectId);

            if (project == null)
                throw new NoNullAllowedException(string.Format("Project with id={0} cannot be deleted", _projectId).ToNotNullErrorMessage());

            var viewModel = project.MapTo<ProjectEditingViewModel>();

            viewModel.Content = project.ProjectContent?.Content;
            viewModel.Name = project.ProjectContent?.Name;
            viewModel.ShortDescription = project.ProjectContent?.ShortDescription;
            //Delete member

            GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(_projectRepository, "ProjectRepository");
            Guard.ArgumentMustMoreThanZero(_projectId, "ProjectId");
        }
    }
}