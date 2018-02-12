namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using System;
    using System.Data;
    using System.Web;
    using System.Web.Mvc;
    using Sattelite.Entities;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.Framework.Extensions;

    public class ProjectEditingPersistence : IProjectEditingPersistence
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectEditingPersistence()
            : this(DependencyResolver.Current.GetService<IProjectRepository>())
        {
        }

        public ProjectEditingPersistence(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// Update project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool SaveProject(Project project)
        {
            var oldIProject = _projectRepository.GetById(project.Id);

            if (oldIProject == null && oldIProject.ProjectContent == null)
                throw new NoNullAllowedException(string.Format("Project with id={0}", project.Id).ToNotNullErrorMessage());

            oldIProject.CategoryId = project.CategoryId;
            //oldIProject.Category= project.Category;
            oldIProject.CoordinatorId = project.CoordinatorId;
            //oldIProject.Coordinator = project.Coordinator;

            if (oldIProject.ProjectContent == null)
                oldIProject.ProjectContent = new ProjectContent();

            if (!oldIProject.ProjectContent.Name.Equals(project.ProjectContent?.Name, StringComparison.InvariantCulture))
                oldIProject.ProjectContent.Name = project.ProjectContent?.Name;

            oldIProject.ProjectContent.ShortDescription = project.ProjectContent?.ShortDescription;
            oldIProject.ProjectContent.Content = project.ProjectContent?.Content; //TODO: links

            //TODO: refresh list of project members : add\remove separately
            //oldIProject.ProjectMembers = project.ProjectMembers;

            //oldIProject.ModifiedBy = project.ModifiedBy;

            return _projectRepository.SaveProject(oldIProject);
        }

        public bool AddProjectMember(int projectId, int userId, int projectRoleId)
        {
            var creatorUser = HttpContext.Current.User.Identity.Name;
            return _projectRepository.AddProjectMember(projectId, userId, projectRoleId, creatorUser);
        }

        public bool AddProjectMember(ProjectMember projectMember)
        {
            return _projectRepository.AddProjectMember(projectMember);
        }

        //public bool DeleteProjectMember(int projectId, int userId)
        //{
        //    return _projectRepository.DeleteProjectMember(projectId, userId);
        //}
    }
}