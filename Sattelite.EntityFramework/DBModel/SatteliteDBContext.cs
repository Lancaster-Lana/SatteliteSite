using System.Data.Entity;
using Sattelite.Entities;
using Sattelite.Mapping;
using Sattelite.Data;
using Sattelite.EntityFramework.DBModel;

namespace Sattelite.EntityFramework
{
    public class SatteliteDBContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<CategorySubscription> Subscriptions { get; set; }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<CategoryPermission> CategoryPermissions { get; set; }
        //public virtual DbSet<UsersInRoles> UsersInRoles { get; set; }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsContent> NewsContents { get; set; }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectContent> ProjectContents { get; set; }
        public virtual DbSet<ProjectRole> ProjectRoles { get; set; }
        public virtual DbSet<ProjectMember> ProjectMembers { get; set; }

        #region Ctors

        static SatteliteDBContext()
        {
            Database.SetInitializer(new CreateDatabaseInitializer());
            //Database.SetInitializer(new CreateDatabaseIfNotExists<SatteliteDBContext>());
            //Database.SetInitializer(new SatteliteDBSInitializer()); //???
        }

        public SatteliteDBContext() : this(CONSTS.DefaultConnectionString)
        {
        }

        public SatteliteDBContext(string connStringName) : base(connStringName)
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = false;
        }

        #endregion

        /// <summary>
        /// Init mappers with DTs
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CategoryMapping());

            modelBuilder.Configurations.Add(new RoleMapping());
            modelBuilder.Configurations.Add(new CategoryPermissionMapping());

            modelBuilder.Configurations.Add(new UserMapping());
            modelBuilder.Configurations.Add(new UserProfileMapping());
            modelBuilder.Configurations.Add(new CategorySubscriptionMapping());
            //modelBuilder.Configurations.Add(new UsersInRolesMapping());

            modelBuilder.Configurations.Add(new NewsMapping());
            modelBuilder.Configurations.Add(new NewsContentMapping());

            modelBuilder.Configurations.Add(new ProjectMapping());
            modelBuilder.Configurations.Add(new ProjectRoleMapping());
            modelBuilder.Configurations.Add(new ProjectContentMapping());
            modelBuilder.Configurations.Add(new ProjectMemberMapping());
        }
    }
}