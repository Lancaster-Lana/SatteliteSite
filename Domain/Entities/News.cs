using System.ComponentModel.DataAnnotations.Schema;

namespace Sattelite.Entities
{
    /// <summary>
    /// Category News
    /// </summary>
    public class News : Entity
    {
        //[Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        //[Required]
        public int? NewsContentId { get; set; }
        [ForeignKey("NewsContentId")]
        public virtual NewsContent NewsContent { get; set; }
    }
}