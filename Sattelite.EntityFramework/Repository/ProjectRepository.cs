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

        public bool DeleteProjectMember(int projectId, int projectMemberId)
        {
            var project = GetById(projectId);

            if (project == null)
                throw new NoNullAllowedException(string.Format("Project with id={0} had not been found", projectId).ToNotNullErrorMessage());
            return DeleteProjectMember(project, projectMemberId);
        }

        public bool DeleteProjectMember(Project project, int projectMemberId)
        {
            var member = project.ProjectMembers.FirstOrDefault(m => m.Id == projectMemberId);
            if (member == null)
                throw new ArgumentException("projectMemberId");
            Delete<ProjectMember>(member);
            return true;

            //project.ProjectMembers.Remove(member);
            //UnitOfWork.SaveChangesAsync();
        }
    }
}