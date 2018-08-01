namespace Sattelite.Web
{
    using Autofac;

    using Sattelite.Framework.Configurations;
    using Sattelite.Framework.Encyption.Impl;
    using Sattelite.Web.App_Start;
    using Sattelite.EntityFramework;
    using Sattelite.EntityFramework.MediaItem;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.EntityFramework.ViewModels.Admin.Persistences;

    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterInstance(new SatteliteDBContext()).As<SatteliteDBContext>().SingleInstance();

            //builder.RegisterType<GenericRepository<Entity>>().AsImplementedInterfaces();
            builder.RegisterType<CategoryRepository>().AsImplementedInterfaces();
            builder.RegisterType<NewsRepository>().AsImplementedInterfaces();
            builder.RegisterType<UserRepository>().AsImplementedInterfaces();
            builder.RegisterType<RoleRepository>().AsImplementedInterfaces();
            builder.RegisterType<ProjectRepository>().AsImplementedInterfaces();
            builder.RegisterType<ProjectRoleRepository>().AsImplementedInterfaces();

            //Wrapped repositories
            builder.RegisterType<UserEditingPersistence>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<RoleCreatingPersistence>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<RoleDeletingPersistence>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<RoleEditingPersistence>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<CategoryCreatingPersistence>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<CategoryDeletingPersistence>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<CategoryEditingPersistence>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<NewsCreatingPersistence>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<NewsDeletingPersistence>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<NewsEditingPersistence>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<ProjectCreatingPersistence>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ProjectDeletingPersistence>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ProjectEditingPersistence>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<ExConfigurationManager>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<MediaItemStorage>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<Encryptor>().AsImplementedInterfaces().SingleInstance();
            MappingConfig.Configure();

            //Database.SetInitializer(new CreateDatabaseIfNotExists<SatteliteDBContext>());
            //Database.SetInitializer(new SeedInitializer());
        }

        //public override void Initialize()
        //{
        //    IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        //}
    }
}