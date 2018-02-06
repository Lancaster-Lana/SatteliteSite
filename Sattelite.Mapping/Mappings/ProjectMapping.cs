namespace Sattelite.Mapping
{
    using Sattelite.Entities;

    public class ProjectMapping : EntityMappingBase<Project>
    {
        public ProjectMapping()
        {
            //this.Property(x => x.CategoryId).IsRequired();
            this.HasRequired(x => x.Category)
                .WithMany(y => y.Projects).HasForeignKey(c => c.CategoryId);

            //this.Property(x => x.ProjectContentId);
            //Fluent API to create 1:1, 1:0, 0:1 relationship bw Project<->ProjectContent
            //this.HasOptional(p => p.ProjectContent)
            //    .WithRequired()//.Map(key => key.MapKey("ProjectContentId"))
            //    .WillCascadeOnDelete();

            //this.HasRequired(x => x.ProjectContent)
            //    .WithRequiredPrincipal().HasForeignKey(key => key.ProjectContentId)
            //    .WillCascadeOnDelete(true);

            this.HasMany(p => p.ProjectMembers)
                .WithRequired(m => m.Project).HasForeignKey(x => x.ProjectId)
                .WillCascadeOnDelete(true); //TODO: ?

            this.ToTable("Project");
        }
    }
}