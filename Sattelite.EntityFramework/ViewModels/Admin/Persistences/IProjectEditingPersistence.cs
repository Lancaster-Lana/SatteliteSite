namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using Sattelite.Entities;

    public interface IProjectEditingPersistence
    {
        bool SaveProject(Project project);

        bool AddProjectMember(Project project, User projectMemberUser, ProjectRole projectRole);
    }
}
