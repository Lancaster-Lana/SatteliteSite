namespace Sattelite.Mapping
{
    using Sattelite.Entities;

    public class RoleMapping : EntityMappingBase<Role>
    {
        public RoleMapping()
        {
            this.Property(x => x.Id).IsRequired();
            this.Property(x => x.Name).IsRequired();
            this.Property(x => x.Description);
            //this.Property(x => x.CategoryPermissions);

            this.ToTable("Role");
        }
    }
}