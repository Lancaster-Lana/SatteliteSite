namespace Sattelite.EntityFramework.ViewModels.Admin.Role
{
    using System.Collections.Generic;
    using Sattelite.Base;
    using Sattelite.Entities;

    public class RoleListViewModel :  BaseListViewModel
    {
        public List<Role> Roles { get; set; } = new List<Role>();


    }
}