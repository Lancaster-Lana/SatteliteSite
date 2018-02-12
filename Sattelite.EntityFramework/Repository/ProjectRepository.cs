using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Sattelite.Entities;
using Sattelite.Framework.Extensions;

namespace Sattelite.EntityFramework.Repository
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository()
           : this(DependencyResolver.Current.GetService<SatteliteDBContext>())
        {
        }

        public ProjectRepository(SatteliteDBContext context) : base(context)
        {
        }

        public Project GetById(int id)
        {
            return GetOneIncluding(x => x.Id == id,
                            "Category",
                            "Coordinator",
                            "ProjectContent",
                            "ProjectMembers");
            //return FindOne(x => x.Id == id,
            //               x => x.ProjectMembers); //with related colletions

        }

        public IEnumerable<Project> GetProjects()
        {
            //var projects = GetIncluding(x => x.CreatedDate.HasValue,
            //                         "ProjectContent",
            //                         "Category",
            //                         "Coordinator",
            //                         "ProjectMembers"
            //                 );

            var projects = GetIncluding(x => x.CreatedDate.HasValue,
                                        s => s.ProjectContent);
            return projects;
        }

        public IEnumerable<Project> GetProjects(int numOfPage)
        {
            return GetProjects().OrderByDescending(x => x.CreatedDate).Take(numOfPage);
        }

        /// <summary>
        /// Get projects by category
        /// </summary>
        /// <param name="categoryId">filterable category</param>
        /// <returns></returns>
        public IEnumerable<Project> GetByCategory(int categoryId)
        {
            var projects = GetProjects();
            return categoryId > 0
                ? projects.Where(x => x.CategoryId == categoryId)
                : projects; //all project without category filter
        }

        public IEnumerable<Project> SeachByName(string filterableName, int index, int numOfpage, out int numOfRecords)
        {
            var projects = GetProjects();
            numOfRecords = projects.Count();
            projects = string.IsNullOrEmpty(filterableName)
                ? projects //return all projects if filter is empty
                : projects.Where(x => x.ProjectContent?.Name != null ? x.ProjectContent.Name.Contains(filterableName) : false);
            var projectsOnPage = projects.Skip((index - 1) * numOfpage).Take(numOfpage);
            return projects;
        }

        /// <summary>
        /// Filter on the top of Index page
        /// </summary>
        /// <param name="nameFilter"></param>
        /// <param name="categoryId">project category</param>
        /// <param name="index"></param>
        /// <param name="numOfpage"></param>
        /// <param name="numOfRecords"></param>
        /// <returns></returns>
        public IEnumerable<Project> SeachByNameAndCategory(string nameFilter, int categoryId, int index, int numOfpage, out int numOfRecords)
        {
            var projects = GetByCategory(categoryId);
            numOfRecords = projects.Count();
            projects = string.IsNullOrEmpty(nameFilter)
                ? projects //return all projects if name filter is empty
                : projects.Where(x => x.ProjectContent?.Name != null ? x.ProjectContent.Name.Contains(nameFilter) : false);
            var projectsOnPage = projects.Skip((index - 1) * numOfpage).Take(numOfpage);
            return projects;
        }

        /// <summary>
        /// Create or update project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool SaveProject(Project project)
        {
            Save(project);
            return true;
        }

        public bool DeleteProject(int projectId)
        {
            var project = GetById(projectId);
            return DeleteProject(project);
        }

        public bool DeleteProject(Project project)
        {
            Delete(project);
            return true;
        }

        #region Membership

        public ProjectMember GetProjectMemberById(int projectMemberId)
        {
            var projectMember = GetByKey<ProjectMember>(projectMemberId);
            return projectMember;
        }

        public bool AddProjectMember(int projectId, int userId, int projectRoleId, string createdBy)
        {
            var project = GetById(projectId);

            if (project == null)
                throw new NoNullAllowedException(string.Format("Project with id={0} had not been found", projectId).ToNotNullErrorMessage());

            return AddProjectMember(project, userId, projectRoleId, createdBy);
        }

        private bool AddProjectMember(Project project, int userId, int projectRoleId, string createdBy)
        {
            var newMember = new ProjectMember
            {
                ProjectId = project.Id,
                ProjectRoleId = projectRoleId,
                UserId = userId,
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now
            };
            project.ProjectMembers.Add(newMember);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool AddProjectMember(ProjectMember projectMember)
        {
            if (projectMember?.ProjectId == null)
                throw new NoNullAllowedException(string.Format("ProjectId is empty").ToNotNullErrorMessage());

            var project = GetById(projectMember.ProjectId.Value);

            if (project == null)
                throw new NoNullAllowedException(string.Format("Project with id={0} had not been found", projectMember.ProjectId).ToNotNullErrorMessage());

            //Check if the user exits in the same role in the project
            bool isUserInTheSameRole = project.ProjectMembers.Any(m => m.UserId == projectMember.UserId 
                                                                    && m.ProjectRoleId == projectMember.ProjectRoleId);

            if (isUserInTheSameRole)
                throw new NoNullAllowedException(string.Format("User '{0}' is a member in the role '{1}' ", projectMember.User.UserName, projectMember.ProjectRole.Name).ToExistErrorMessage());

            //Save<ProjectMember> 
            project.ProjectMembers.Add(projectMember);
            UnitOfWork.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete member from a role 
        /// (NOTE: user may stay assigned to another role in the same project)
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userId"></param>
        /// <param name="projectMemberRole"></param>
        /// <returns></returns>
        public bool DeleteProjectMember(int projectId, int userId, int projectMemberRole)
        {
            var project = GetById(projectId);

            if (project == null)
                throw new NoNullAllowedException(string.Format("Project with id={0} had not been found", projectId).ToNotNullErrorMessage());

            var projectMember = project.ProjectMembers.FirstOrDefault(m => m.UserId == userId && m.ProjectRoleId == projectMemberRole);
            if (projectMember == null)
                throw new NoNullAllowedException(string.Format("Project member with userId={0} and projectMemberRole ={1} had not been found", userId, projectMemberRole).ToNotNullErrorMessage());

            return DeleteProjectMember(projectMember);
        }

        public bool DeleteProjectMember(int projectMemberId)
        {
            if (projectMemberId <= 0)
                throw new ArgumentException("projectMemberId");

            var projectMember = GetByKey<ProjectMember>(projectMemberId);
            if (projectMember == null)
                throw new NoNullAllowedException(string.Format("Project member with id={0} had not been found", projectMemberId).ToNotNullErrorMessage());

            return DeleteProjectMember(projectMember);
        }

        public bool DeleteProjectMember(ProjectMember projectMember)
        {
            Delete<ProjectMember>(projectMember);
            return true;
        }

        #endregion
    }
}