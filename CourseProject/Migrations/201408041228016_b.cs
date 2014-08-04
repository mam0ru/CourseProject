namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Formulae",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Task_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercises", t => t.Task_Id)
                .Index(t => t.Task_Id);
            
            CreateTable(
                "dbo.Graphs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Task_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercises", t => t.Task_Id)
                .Index(t => t.Task_Id);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Task_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercises", t => t.Task_Id)
                .Index(t => t.Task_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Task_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercises", t => t.Task_Id)
                .Index(t => t.Task_Id);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Task_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercises", t => t.Task_Id)
                .Index(t => t.Task_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "Task_Id", "dbo.Exercises");
            DropForeignKey("dbo.Tags", "Task_Id", "dbo.Exercises");
            DropForeignKey("dbo.Pictures", "Task_Id", "dbo.Exercises");
            DropForeignKey("dbo.Graphs", "Task_Id", "dbo.Exercises");
            DropForeignKey("dbo.Formulae", "Task_Id", "dbo.Exercises");
            DropIndex("dbo.Videos", new[] { "Task_Id" });
            DropIndex("dbo.Tags", new[] { "Task_Id" });
            DropIndex("dbo.Pictures", new[] { "Task_Id" });
            DropIndex("dbo.Graphs", new[] { "Task_Id" });
            DropIndex("dbo.Formulae", new[] { "Task_Id" });
            DropTable("dbo.Videos");
            DropTable("dbo.Tags");
            DropTable("dbo.Pictures");
            DropTable("dbo.Graphs");
            DropTable("dbo.Formulae");
        }
    }
}
