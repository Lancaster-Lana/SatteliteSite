using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sattelite.Entities
{
    public class ProjectContent : Entity
    {
        //[Key]
        //public virtual int ProjectContentId { get; set; }
        //[ForeignKey("ProjectId")]
        //public virtual Project Project { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        /// <summary>
        /// Project details: links on sources, etc.
        /// </summary>
        public string Content { get; set; }

        //Related, linked projects documnents, etc
        //public virtual IList<Project> SubProjects { get; set; }  = new List<Project>();    
    }
}