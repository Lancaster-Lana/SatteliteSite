namespace Sattelite.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Description = c.String(maxLength: 200),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        NumOfView = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        NewsContentId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        NumOfView = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NewsContent", t => t.NewsContentId)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.NewsContentId);
            
            CreateTable(
                "dbo.NewsContent",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        ShortDescription = c.String(nullable: false),
                        Content = c.String(),
                        SmallImage = c.String(),
                        MediumImage = c.String(),
                        BigImage = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        NumOfView = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryPermission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DisplayName = c.String(),
                        Description = c.String(),
                        CategoryId = c.Int(nullable: false),
                        ReadOnly = c.Boolean(nullable: false),
                        Edit = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        NumOfView = c.Long(),
                        Role_Id = c.Int(),
                        ProjectRole_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.Role_Id)
                .ForeignKey("dbo.ProjectRole", t => t.ProjectRole_Id)
                .Index(t => t.CategoryId)
                .Index(t => t.Role_Id)
                .Index(t => t.ProjectRole_Id);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        CoordinatorId = c.Int(),
                        ProjectContentId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        NumOfView = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.CoordinatorId)
                .ForeignKey("dbo.ProjectContent", t => t.ProjectContentId)
                .Index(t => t.CategoryId)
                .Index(t => t.CoordinatorId)
                .Index(t => t.ProjectContentId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        DisplayName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        RoleId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        NumOfView = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        NumOfView = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategorySubscription",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        UserId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        NumOfView = c.Long(),
                        UserProfile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.UserProfile", t => t.UserProfile_Id)
                .Index(t => t.CategoryId)
                .Index(t => t.UserId)
                .Index(t => t.UserProfile_Id);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(),
                        Gender = c.Boolean(),
                        Birthday = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        NumOfView = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ProjectContent",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ShortDescription = c.String(nullable: false),
                        Content = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        NumOfView = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectMember",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        ProjectRoleId = c.Int(),
                        UserId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        NumOfView = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectRole", t => t.ProjectRoleId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.ProjectId)
                .Index(t => t.ProjectRoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjectRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        NumOfView = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectMember", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.ProjectMember", "UserId", "dbo.User");
            DropForeignKey("dbo.ProjectMember", "ProjectRoleId", "dbo.ProjectRole");
            DropForeignKey("dbo.CategoryPermission", "ProjectRole_Id", "dbo.ProjectRole");
            DropForeignKey("dbo.Project", "ProjectContentId", "dbo.ProjectContent");
            DropForeignKey("dbo.Project", "CoordinatorId", "dbo.User");
            DropForeignKey("dbo.UserProfile", "Id", "dbo.User");
            DropForeignKey("dbo.CategorySubscription", "UserProfile_Id", "dbo.UserProfile");
            DropForeignKey("dbo.CategorySubscription", "UserId", "dbo.User");
            DropForeignKey("dbo.CategorySubscription", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.User", "RoleId", "dbo.Role");
            DropForeignKey("dbo.CategoryPermission", "Role_Id", "dbo.Role");
            DropForeignKey("dbo.Project", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.CategoryPermission", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.News", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.News", "NewsContentId", "dbo.NewsContent");
            DropIndex("dbo.ProjectMember", new[] { "UserId" });
            DropIndex("dbo.ProjectMember", new[] { "ProjectRoleId" });
            DropIndex("dbo.ProjectMember", new[] { "ProjectId" });
            DropIndex("dbo.UserProfile", new[] { "Id" });
            DropIndex("dbo.CategorySubscription", new[] { "UserProfile_Id" });
            DropIndex("dbo.CategorySubscription", new[] { "UserId" });
            DropIndex("dbo.CategorySubscription", new[] { "CategoryId" });
            DropIndex("dbo.User", new[] { "RoleId" });
            DropIndex("dbo.Project", new[] { "ProjectContentId" });
            DropIndex("dbo.Project", new[] { "CoordinatorId" });
            DropIndex("dbo.Project", new[] { "CategoryId" });
            DropIndex("dbo.CategoryPermission", new[] { "ProjectRole_Id" });
            DropIndex("dbo.CategoryPermission", new[] { "Role_Id" });
            DropIndex("dbo.CategoryPermission", new[] { "CategoryId" });
            DropIndex("dbo.News", new[] { "NewsContentId" });
            DropIndex("dbo.News", new[] { "CategoryId" });
            DropTable("dbo.ProjectRole");
            DropTable("dbo.ProjectMember");
            DropTable("dbo.ProjectContent");
            DropTable("dbo.UserProfile");
            DropTable("dbo.CategorySubscription");
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.Project");
            DropTable("dbo.CategoryPermission");
            DropTable("dbo.NewsContent");
            DropTable("dbo.News");
            DropTable("dbo.Category");
        }
    }
}
