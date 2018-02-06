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

    public class ProjectEditingViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly int _projectId;
        private readonly IProjectRepository _projectRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public ProjectEditingViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                int projectId)
            : this(viewNameExpression, projectId,
                  DependencyResolver.Current.GetService<IProjectRepository>(),
                  DependencyResolver.Current.GetService<ICategoryRepository>(),
                  DependencyResolver.Current.GetService<IUserRepository>())
        {
        }

        public ProjectEditingViewModelActionResult(
            Expression<Func<TController, ActionResult>> viewNameExpression, int projectId,
            IProjectRepository projectRepository,
            ICategoryRepository categoryRepository,
            IUserRepository userRepository
            )
            : base(viewNameExpression)
        {
            _projectId = projectId;
            _projectRepository = projectRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var project = _projectRepository.GetById(_projectId);

            if (project == null)
                throw new NoNullAllowedException(string.Format("Project with id={0}", _projectId).ToNotNullErrorMessage());

            var viewModel = project.MapTo<ProjectEditingViewModel>();

            //viewModel.Name = project.ProjectContent?.Name;
            //viewModel.ShortDescription = project.ProjectContent?.ShortDescription;
            //viewModel.Content = project.ProjectContent?.Content;

            //viewModel.CategoryId = project.CategoryId;// project.CategoryId.HasValue ? project.CategoryId.Value : -1;
            //viewModel.CoordinatorId = project.CoordinatorId;
            //viewModel.ProjectMembers = project.ProjectMembers?.ToList();

            GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentMustMoreThanZero(_projectId, "ProjectId");
            Guard.ArgumentNotNull(_projectRepository, "ProjectRepository");
        }

        #endregion
    }
}