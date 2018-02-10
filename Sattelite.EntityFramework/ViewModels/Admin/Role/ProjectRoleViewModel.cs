using Sattelite.Base;
using System.ComponentModel.DataAnnotations;

namespace Sattelite.EntityFramework.ViewModels.Admin.Role
{
    public class ProjectRoleViewModel : BaseViewModel
    {
        public int? RoleId { get; set; }

        [Display(Name = "Назва")]
        [Required]
        public string Name { get; set; }

        //[Display(Name = "Опис")]
        //public string Description { get; set; } = ShortDescription

        //public List<ProjectPermission> ProjectPermissions { get; set; } = new List<ProjectPermission>();
    }
}