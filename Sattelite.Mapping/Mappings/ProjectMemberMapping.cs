namespace Sattelite.Mapping
{
    using Sattelite.Entities;

    public class ProjectMemberMapping : EntityMappingBase<ProjectMember>
    {
        public ProjectMemberMapping()
        {
            this.Property(x => x.ProjectId).IsRequired();
            this.Property(x => x.UserId).IsRequired();
            this.Property(x => x.ProjectRoleId).IsOptional(); //Role can be assigned to user later

            // Map one-to-zero or one relationship 
            //this.HasRequired(m => m.ProjectRole).WithMany();
                //.WithOptional()//role => role.ProjectMember);
                //.WillCascadeOnDelete(false);

            //this.HasRequired(m => m.User).WithMany(); //user may be a multiply-member of several projects
                //.WithOptional() // User may be or not to be a project member (only user)
                //.WillCascadeOnDelete(false);
            //this.Property(x => x.UserId).IsRequired(); //Constains no action on USER delete
            //this.HasRequired(x => x.User).WithRequiredDependent(x => x.ProjectMember).WillCascadeOnDelete(true);
            //this.HasRequired(m => m.User).WithMany(u => u.ProjectMember)

            this.ToTable("ProjectMember");
        }
    }
}