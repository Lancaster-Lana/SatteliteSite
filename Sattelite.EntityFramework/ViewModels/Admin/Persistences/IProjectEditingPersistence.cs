namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using Sattelite.Entities;

    public interface IProjectEditingPersistence
    {
        bool SaveProject(Project project);

        bool AddProjectMember(int projectId, int userId, int projectRoleId);

        bool AddProjectMember(ProjectMember newProjectMember);
    }
}
