using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sattelite.Entities
{
    /// <summary>
    /// System roles: admin, registered user
    /// Project roles:
    /// - Project manager (maybe in several projects)
    /// - Solution Architect
    /// -.Solution Development Engineer
    /// - QA
    /// - Tech writer
    /// - Designer
    /// </summary>
    public class ProjectRole  : Entity
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        //[ForeignKey("RoleId")]
        /// <summary>
        ///TODO: remove For example, politics
        /// </summary>
        public virtual ICollection<CategoryPermission> CategoryPermissions { get; set; } = new List<CategoryPermission>();
    }
}