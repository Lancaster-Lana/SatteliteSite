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

    public class ProjectDeletingViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly int _projectId;
        private readonly IProjectRepository _projectRepository;

        public ProjectDeletingViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                int projectId)
            : this(viewNameExpression, projectId,
                  DependencyResolver.Current.GetService<IProjectRepository>())
        {
        }

        public ProjectDeletingViewModelActionResult(
            Expression<Func<TController, ActionResult>> viewNameExpression, int projectId,
            IProjectRepository projectRepository)
            : base(viewNameExpression)
        {
            _projectId = projectId;
            _projectRepository = projectRepository;
        }

        #endregion

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var project = _projectRepository.GetById(_projectId);

            if (project == null)
                throw new NoNullAllowedException(string.Format("Project with id={0} cannot be deleted", _projectId).ToNotNullErrorMessage());

            var viewModel = project.MapTo<ProjectViewModel>();

            //does not matter content
            //viewModel.Name = project.ProjectContent?.Name;
            //viewModel.ShortDescription = project.ProjectContent?.ShortDescription;
            //viewModel.Content = project.ProjectContent?.Content;

            GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(_projectRepository, "ProjectRepository");
            Guard.ArgumentMustMoreThanZero(_projectId, "ProjectId");
        }
    }
}