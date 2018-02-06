using Sattelite.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sattelite.EntityFramework.ViewModels.Admin.Role
{
    public class RoleViewModel
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