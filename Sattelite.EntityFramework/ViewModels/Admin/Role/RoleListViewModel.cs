namespace Sattelite.EntityFramework.ViewModels.Admin.Role
{
    using System.Collections.Generic;
    using Sattelite.Base;
    using Sattelite.Entities;

    public class RoleListViewModel :  BaseListViewModel
    {
        /// <summary>
        /// site roles
        /// </summary>
        public List<Role> Roles { get; set; } = new List<Role>();

        /// <summary>
        /// peoject member roles
        /// </summary>
        public List<ProjectRole> ProjectRoles { get; set; } = new List<ProjectRole>();
    }
}