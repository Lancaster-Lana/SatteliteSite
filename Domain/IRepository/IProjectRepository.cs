namespace Sattelite.EntityFramework.Repository
{
    using Sattelite.Entities;
    using System.Collections.Generic;

    public interface IProjectRepository
    {
        Project GetById(int id);

        IEnumerable<Project> GetProjects();

        IEnumerable<Project> GetProjects(int numOfPage);

        IEnumerable<Project> SeachByName(string name, int index, int numOfpage, out int numOfRecords);

        IEnumerable<Project> GetByCategory(int categoryId);

        IEnumerable<Project> SeachByNameAndCategory(string name, int categoryId, int index, int numOfpage, out int numOfRecords);

        bool SaveProject(Project project);

        bool DeleteProject(int projectId);

        bool DeleteProject(Project project);

        bool DeleteProjectMember(int projectId, int projectMemberId);

        bool DeleteProjectMember(Project project, int projectMemberId);
    }
}