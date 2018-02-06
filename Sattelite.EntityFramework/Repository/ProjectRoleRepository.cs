using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Sattelite.Entities;
using Sattelite.Entities.ProjectAgg;

namespace Sattelite.EntityFramework.Repository
{
    public class ProjectRoleRepository : GenericRepository<ProjectRole>, IProjectRoleRepository
    {
        public ProjectRoleRepository()
           : this(DependencyResolver.Current.GetService<SatteliteDBContext>())
        {
        }

        public ProjectRoleRepository(SatteliteDBContext context) : base(context)
        {
        }

        public ProjectRole GetById(int id)
        {
            return GetProjectRoles().First(x => x.Id == id);
        }

        public IEnumerable<ProjectRole> GetProjectRoles()
        {
            var projectRoles = GetAll();
            return projectRoles;
        }

        public IEnumerable<ProjectRole> GetProjectRoles(int numOfPage)
        {
            return GetProjectRoles().OrderByDescending(x => x.CreatedDate).Take(numOfPage);
        }

        public IEnumerable<ProjectRole> SeachByName(string name, int index, int numOfpage, out int numOfRecords)
        {
            var projectRoles = GetProjectRoles();

            numOfRecords = projectRoles.Count();

            projectRoles = projectRoles.Where(x => x.Name.Contains(name));

            return projectRoles.Skip((index - 1) * numOfpage).Take(numOfpage);
        }

        public bool SaveProjectRole(ProjectRole projectRole)
        {
            Save(projectRole);
            return true;
        }

        public bool DeleteProjectRole(int id)
        {
            var projectRole = GetById(id);
            return DeleteProjectRole(projectRole);
        }

        public bool DeleteProjectRole(ProjectRole projectRole)
        {
            Delete(projectRole);
            return true;
        }
    }
}