using Sattelite.Entities.UserAgg;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sattelite.Entities
{
    public class CategorySubscription : Entity
    {
        public virtual int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        //[Column("UserId")]
        public virtual int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        //public virtual string UserName { get; set; }
    }
}
