namespace Sattelite.Mapping
{
    using Sattelite.Entities;

    public class CategoryPermissionMapping : EntityMappingBase<CategoryPermission>
    {
        public CategoryPermissionMapping()
        {
            this.Property(x => x.Name).IsRequired();
            this.Property(x => x.CategoryId).IsRequired();
            //this.HasMany(x => x.Roles); several roles may have this permission
            this.ToTable("CategoryPermission");
        }
    }
}