using Sattelite.EntityFramework.Migrations;
using System.Data.Entity;
using System;

namespace Sattelite.EntityFramework.DBModel
{
    public class CreateDatabaseInitializer : CreateDatabaseIfNotExists<SatteliteDBContext>, IDatabaseInitializer<SatteliteDBContext>
    {
        public override void InitializeDatabase(SatteliteDBContext context)
        {
            base.InitializeDatabase(context);

            Console.WriteLine("Seeding...");

            //seed database
            Seeder.Seed(context);

            Console.WriteLine("Seeding done");
        }
    } 
    
    //public class SatteliteDBSInitializer : IDatabaseInitializer<SatteliteDBContext>
   // {
   //     public void InitializeDatabase(SatteliteDBContext context)
   //     {
   //         Seeder.Seed(context);
   //     }
   // }
}
