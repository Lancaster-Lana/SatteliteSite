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

        #region Project Membership

        ProjectMember GetProjectMemberById(int projectMemberId);

        bool AddProjectMember(int projectId, int userId, int projectRoleId, string createdBy);

        bool AddProjectMember(ProjectMember projectMember);

        bool DeleteProjectMember(int projectMemberId);

        bool DeleteProjectMember(ProjectMember projectMember);

        /// <summary>
        /// Remove user from a role in the project.
        /// NOTE. this user may stay in another projectMemberRole in this project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userId"></param>
        /// <param name="projectMemberRole"></param>
        /// <returns></returns>
        bool DeleteProjectMember(int projectId, int userId, int projectMemberRoleId);

        #endregion
    }
}