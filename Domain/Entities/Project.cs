using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sattelite.Entities
{
    public class Project : Entity
    {
        //[Required]
        //public string Name { get; set; } // from ProjectContent

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public int? CoordinatorId { get; set; }
        [ForeignKey("CoordinatorId")]
        public virtual User Coordinator { get; set; }

        public int? ProjectContentId { get; set; } //- if want to create navigation property
        [ForeignKey("ProjectContentId")]
        public virtual ProjectContent ProjectContent { get; set; }

        /// <summary>
        /// Participants of the project
        /// </summary>
        //[ForeignKey("ProjectId")]
        public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
    }
}
