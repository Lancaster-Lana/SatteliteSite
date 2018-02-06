using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sattelite.Entities
{
    public class UserProfile : Entity// : AbpUser<UserProfile> //: FullAuditedEntity<long> 
    {
        //public long UserId { get; set; }
        /// <summary>
        /// UserProfile is for some application User
        /// </summary>
        //[ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return LastName + ' ' + FirstName;
            }
        }

        public string Email { get; set; }

        /// <summary>
        /// 0 -male, 1-female
        /// </summary>
        public bool? Gender { get; set; }

        //[Range(new DateTime(1900, 1, 1), DateTime.Now]
        public DateTime? Birthday { get; set; }

        //[Range(1, 100)]
        //public uint Age { get; set; }

        //public virtual Address AddressContact { get; set; }

        //public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();

        /// <summary>
        /// Several social contacts, like : linkedin, facebook, yachoo,  
        /// </summary>
        //public virtual ICollection<Contact> UserContacts { get; set; } = new List<Contact>();

        /// <summary>
        /// 
        /// </summary>
        //public virtual ICollection<UserGroup> PersonalGroups { get; set; } = new List<UserGroup>();

        // TODO: or list of projects roles : in different projects - different roles
        //public virtual ICollection<ProjectRole> ProjectRoles { get; set; } = new List<ProjectRole>();

        /// <summary>
        /// Categories to which user is subscribed
        /// </summary>
        //[ForeignKey("UserId")]
        public virtual ICollection<CategorySubscription> Subscriptions { get; set; } = new List<CategorySubscription>();
    }
}
