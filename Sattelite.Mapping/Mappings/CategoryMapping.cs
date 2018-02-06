namespace Sattelite.Mapping
{
    using Sattelite.Entities;

    public class CategoryMapping : EntityMappingBase<Category>
    {
        public CategoryMapping()
        {
            this.Property(x => x.Name).IsRequired();

            this.HasMany(x => x.News)
                .WithRequired(y => y.Category).HasForeignKey(p => p.CategoryId)
                .WillCascadeOnDelete();//delete category news

            this.HasMany(x => x.Projects)
                .WithRequired(p => p.Category).HasForeignKey(p => p.CategoryId)
                .WillCascadeOnDelete(); //left category projects (can be re-assigned to another category)

            this.ToTable("Category");
        }
    }
}