namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using System.Web.Mvc;

    using Sattelite.Entities;
    using Sattelite.EntityFramework.Repository;

    public class ProjectCreatingPersistence : IProjectCreatingPersistence
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectCreatingPersistence()
            : this(DependencyResolver.Current.GetService<IProjectRepository>())
        {
        }

        public ProjectCreatingPersistence(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public bool CreateProject(Project project)
        {
            return _projectRepository.SaveProject(project);
        }
    }
}