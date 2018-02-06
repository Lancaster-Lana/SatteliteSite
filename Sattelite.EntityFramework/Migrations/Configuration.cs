namespace Sattelite.EntityFramework.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<SatteliteDBContext> //DropCreateDatabaseIfModelChanges<SatteliteDBContext> 
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SatteliteDBContext context)
        {
            Seeder.Seed(context);
        }
    }
}
