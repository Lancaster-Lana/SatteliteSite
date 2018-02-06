
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sattelite.Entities
{
    public class User : Entity
    {
        //[Display(Name = "Ім'я користувача")]
        [Required]
        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        /// <summary>
        /// Site role (not project role). See DefaultRoles
        /// </summary>
        public int RoleId { get; set; } = 3; //DefaultRoles.RegisteredUser
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; } // TODO: list of project roles

        //public long UserId { get; set; }
        //[ForeignKey("UserId")]
        public virtual UserProfile UserProfile { get; set; }

        //[ForeignKey("UserId")]
        public virtual ICollection<CategorySubscription> Subscriptions { get; set; } = new List<CategorySubscription>();
    }
}