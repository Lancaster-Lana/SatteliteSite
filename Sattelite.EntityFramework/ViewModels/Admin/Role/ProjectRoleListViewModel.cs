namespace Sattelite.EntityFramework.ViewModels.Admin.Role
{
    using System.Collections.Generic;
    using Sattelite.Base;
    using Sattelite.Entities;

    public class ProjectRoleListViewModel :  BaseListViewModel
    {
        public List<ProjectRole> ProjectRoles { get; set; } = new List<ProjectRole>();
    }
}