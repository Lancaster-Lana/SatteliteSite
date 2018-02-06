using Sattelite.Entities.UserAgg;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sattelite.Entities
{
    /// <summary>
    /// Participant of the project.
    /// For example in one project may have one role, in other - other one/
    /// </summary>
    public class ProjectMember : Entity
    {
        public virtual int? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public virtual int? ProjectRoleId { get; set; }
        /// <summary>
        /// Role of the user in specific project 
        /// TODO: may have several roles
        /// </summary>
        [ForeignKey("ProjectRoleId")]
        public virtual ProjectRole ProjectRole { get; set; }

        public virtual int UserId { get; set; }
        [ForeignKey("UserId")] //[ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}