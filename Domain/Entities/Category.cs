using Sattelite.Entities.UserAgg;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sattelite.Entities
{
    public class Category : Entity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ICollection<CategoryPermission> Permissions { get; set; } = new List<CategoryPermission>();

        /// <summary>
        /// Related category's news
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual ICollection<News> News { get; set; } = new List<News>();

        /// <summary>
        /// Related projects
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

        /// <summary>
        /// All user subscriptions for this category
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual ICollection<CategorySubscription> Subscriptions { get; set; } = new List<CategorySubscription>();
    }
}
