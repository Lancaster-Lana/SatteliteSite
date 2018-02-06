using System;
using Sattelite;
using Sattelite.Entities;
using System.Data.Entity.Migrations;

namespace Sattelite.EntityFramework.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public class Seeder
    {
        /// <summary>
        /// Fill up DB with default values
        /// </summary>
        /// <param name="context"></param>
        public static void Seed(SatteliteDBContext context)
        {
            //1.1 CREATE DEFAULT SITE ROLES
            context.Roles.AddOrUpdate(c => c.Name,
                new Role { Name = "Admin", Description = "can create roles, users, update permissions ", CreatedDate = DateTime.Now, CreatedBy = "admin" },
                new Role { Name = "ContentManager", Description = "can add some news, their content", CreatedDate = DateTime.Now, CreatedBy = "admin" },
                new Role { Name = "Registered user", Description = "Simple user of the site", CreatedDate = DateTime.Now, CreatedBy = "admin" }
              );

            //1.2 CREATE DEFAULT PROJECT ROLES
            context.ProjectRoles.AddOrUpdate(c => c.Name,
                new ProjectRole { Name = "Coordinator", Description = "Ideas generator (loke owner), building\\planing : team hierarchy, team members. Coordinating progress of the project, planning scheduler of releases. Reacting\\informing about external changes", CreatedDate = DateTime.Now, CreatedBy = "admin" },
                new ProjectRole { Name = "ProjectAdministrator", Description = "Supply hardware and software installations, administration users rights", CreatedDate = DateTime.Now, CreatedBy = "admin" },
                new ProjectRole { Name = "ProjectManager", Description = "PM, managing spring tasks, coordinate ", CreatedDate = DateTime.Now, CreatedBy = "admin" },
                new ProjectRole { Name = "DevOps", Description = "Continues integration in project. Servers installation. Publishing solution on the cloud platforms (Azure, AWS). Coordination with PM and SQA teams", CreatedDate = DateTime.Now, CreatedBy = "admin" },

                new ProjectRole { Name = "Architector", Description = "Architecting solution of the application due to business requirements, choosing API and frameworks, development structure of the project. Using UML diagrams", CreatedDate = DateTime.Now, CreatedBy = "admin" },
                new ProjectRole { Name = "BusinessAnalyst", Description = "Analysys business requirements", CreatedDate = DateTime.Now, CreatedBy = "admin" },
                new ProjectRole { Name = "UIDesigner", Description = "UI design, visualization by diagramming, sketchflows. Using Visio, InVison, SketchFlow", CreatedDate = DateTime.Now, CreatedBy = "admin" },
                new ProjectRole { Name = "SolutionEngineer", Description = "Solution implementation by requirements", CreatedDate = DateTime.Now, CreatedBy = "admin" },
                new ProjectRole { Name = "SQA", Description = "Solution testing duw to requirements, test cases development, automation testing", CreatedDate = DateTime.Now, CreatedBy = "admin" },
                new ProjectRole { Name = "TechnicalWriter", Description = "Writing technical documentation for the product, user guides", CreatedDate = DateTime.Now, CreatedBy = "admin" },

                new ProjectRole { Name = "SupportEngineer", Description = "Working with client issues", CreatedDate = DateTime.Now, CreatedBy = "admin" },
                new ProjectRole { Name = "SalesManager", Description = "working with clients, contacts exchange, tracking suggestion\\issues", CreatedDate = DateTime.Now, CreatedBy = "admin" }
                );

            //CREATE ADMIN USER
            context.Users.AddOrUpdate(c => c.UserName,
                    new User
                    {
                        UserName = DefaultAdminAccount.DefaultUser,
                        DisplayName = DefaultAdminAccount.DefaultUser,
                        Email = DefaultAdminAccount.DefaultEmail,
                        Password = DefaultAdminAccount.DefaultPassword,
                        RoleId = (int)DefaultRoles.Admin,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "admin"
                    }
              );

            //context.Categories.AddRange
            //CREATE DEFAULT CATEGORIES
            context.Categories.AddOrUpdate(c => c.Name,
                new Category { Name = "Politics", Description = "Political news, human rights in different countries", CreatedDate = DateTime.Now, CreatedBy = "admin" },
                new Category { Name = "Economics", Description = "Economical trends (block-chain, bitcoins), events and trainings", CreatedDate = DateTime.Now, CreatedBy = "admin" },
                new Category { Name = "Eco-Technologies", Description = "Wind & solar systems", CreatedDate = DateTime.Now, CreatedBy = "admin" },
                new Category { Name = "IT Innovations", Description = "", CreatedDate = DateTime.Now, CreatedBy = "admin" },
                new Category { Name = "Education", Description = "Education trends, innovations in online-events", CreatedDate = DateTime.Now, CreatedBy = "admin" },
                new Category { Name = "Culture", Description = "Art, culture events", CreatedDate = DateTime.Now, CreatedBy = "admin" }
              );
        }
    }
}
