namespace Sattelite.EntityFramework.Repository
{
    using Sattelite.Entities;
    using System.Collections.Generic;

    public interface IProjectRoleRepository
    {
        ProjectRole GetById(int id);

        IEnumerable<ProjectRole> GetProjectRoles();

        IEnumerable<ProjectRole> GetProjectRoles(int numOfPage);

        IEnumerable<ProjectRole> SeachByName(string name, int index, int numOfpage, out int numOfRecords);

        bool SaveProjectRole(ProjectRole projectRole);

        bool DeleteProjectRole(int projectRoleId);

        bool DeleteProjectRole(ProjectRole projectRole);
    }
}