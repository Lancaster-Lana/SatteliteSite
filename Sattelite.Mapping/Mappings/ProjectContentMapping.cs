namespace Sattelite.Mapping
{
    using Sattelite.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class ProjectContentMapping :EntityMappingBase<ProjectContent>// EntityTypeConfiguration<ProjectContent>//
    {
        public ProjectContentMapping()
        {
            this.Property(x => x.Name).IsRequired();
            this.Property(x => x.ShortDescription).IsRequired();
            this.Property(x => x.Content);
            this.Property(x => x.NumOfView);

            //Fluent API to create 1:1 relationship to Project<->ProjectContent
            //this.HasRequired(c => c.Project)
            //    .WithRequiredPrincipal(p => p.ProjectContent).WillCascadeOnDelete();

            this.ToTable("ProjectContent");
        }
    }  
}