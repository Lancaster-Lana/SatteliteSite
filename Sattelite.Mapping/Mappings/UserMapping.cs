namespace Sattelite.Mapping
{
    using Sattelite.Entities;

    public class UserMapping : EntityMappingBase<User>
    {
        public UserMapping()
        {
            //Fluent API for 1<->1 user - profile
            //this.HasKey(t => t.UserProfileId); // Has FK, if has for
            this.HasOptional(e => e.UserProfile) //Optional - to be ceated\filled up later
                .WithRequired(e => e.User)
                .WillCascadeOnDelete();

            this.Property(x => x.UserName).IsRequired();
            this.Property(x => x.RoleId).IsRequired(); //this.Property(x => x.Role);
            this.Property(x => x.DisplayName);
            this.Property(x => x.Password);
            this.Property(x => x.Email);

            this.ToTable("User");
        }
    }
}