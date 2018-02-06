using Sattelite.Entities;

namespace Sattelite.Mapping
{
    public class UserProfileMapping : EntityMappingBase<UserProfile>
    {
        public UserProfileMapping()
        {
            this.Property(x => x.FirstName).IsRequired();
            this.Property(x => x.LastName).IsRequired();

            //Fluent API for 1<->1 user - profile
            //this.HasRequired(t => t.User)
            //    .WithRequiredPrincipal(t => t.UserProfile);

            this.ToTable("UserProfile");
        }
    }
}
