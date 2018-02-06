using Sattelite.Base;
using Sattelite.EntityFramework.ViewModels.Admin.User;

namespace Sattelite.EntityFramework.ViewModels.Admin.Project
{
    public class ProjectMemberViewModel : BaseViewModel
    {
        public int ProjectId { get; set; }

        public UserViewModel ProjectUser { get; set; }

        /// <summary>
        /// Member role in this project
        /// </summary>
        public ProjectMemberRoleViewModel ProjectRole { get; set; }
    }
}
