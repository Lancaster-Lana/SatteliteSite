
using System.ComponentModel.DataAnnotations;

namespace Sattelite.EntityFramework.ViewModels.Admin.User
{
    public class UserViewModel
    {
        public int? UserId { get; set; }

        [Required]
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        //[Required]
        //[Compare("Password", ErrorMessage = "Password doesn't match")]
        //public string ConfirmPassword { get; set; }

        /// <summary>
        /// Default role is 'RegisterUser'
        /// </summary>
        public int RoleId { get; set; }
        //public RoleViewModel Role { get; set; }

        //public IList<ProjectRoleViewModel> ProjectRoles { get; set; }
    }
}