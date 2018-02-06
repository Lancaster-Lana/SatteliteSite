using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sattelite.Entities
{
    /// <summary>
    /// Site roles: admin, registered user, content manager
    /// </summary>
    public class Role : Entity
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        //[ForeignKey("RoleId")]
        public virtual ICollection<CategoryPermission> CategoryPermissions { get; set; } = new List<CategoryPermission>();
    }
}