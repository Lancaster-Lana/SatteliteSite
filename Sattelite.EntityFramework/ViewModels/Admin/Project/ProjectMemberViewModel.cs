using Sattelite.Base;
using Sattelite.EntityFramework.ViewModels.Admin.User;

namespace Sattelite.EntityFramework.ViewModels.Admin.Project
{
    public class ProjectMemberViewModel : BaseViewModel
    {
        public int? Id { get; set; }

        public int ProjectId { get; set; }

        public int UserId { get; set; }
        public UserViewModel ProjectUser { get; set; }

        /// <summary>
        /// Member role in this project
        /// </summary>
        public int ProjectRoleId { get; set; }
        public ProjectMemberRoleViewModel ProjectRole { get; set; }
    }
}
