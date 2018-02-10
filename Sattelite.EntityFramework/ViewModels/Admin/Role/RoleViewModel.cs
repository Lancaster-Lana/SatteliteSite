using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sattelite.Base;
using Sattelite.Entities;

namespace Sattelite.EntityFramework.ViewModels.Admin.Role
{
    public class RoleViewModel //: BaseViewModel
    {
        public int? RoleId { get; set; }

        [Display(Name = "Назва")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Опис")]
        public string Description { get; set; }

        public List<CategoryPermission> Permissions { get; set; } = new List<CategoryPermission>();
    }
}