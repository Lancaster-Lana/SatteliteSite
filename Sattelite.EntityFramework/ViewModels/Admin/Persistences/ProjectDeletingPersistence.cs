namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using System.Data;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.Repository;

    public class ProjectDeletingPersistence : IProjectDeletingPersistence
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectDeletingPersistence(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public bool DeleteProject(int id)
        {
            return _projectRepository.DeleteProject(id);
        }

        public bool DeleteProjectMember(int projectId, int projectMemberId)
        {
            return _projectRepository.DeleteProjectMember(projectId, projectMemberId);
        }
    }
}