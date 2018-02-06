using Sattelite.Entities;

namespace Sattelite.Mapping
{
    public class ProjectRoleMapping : EntityMappingBase<ProjectRole>
    {
        public ProjectRoleMapping()
        {
            this.Property(x => x.Id).IsRequired();
            this.Property(x => x.Name).IsRequired();
            this.Property(x => x.Description);
            //this.Property(x => x.PrrojectResponsibilities); //TODO:
            //this.Property(x => x.PrrojectPermissions); //TODO:

            this.ToTable("ProjectRole");
        }
    }
}
