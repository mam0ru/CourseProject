namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Exercises", "Category_Id", c => c.Int());
            AddColumn("dbo.Exercises", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Exercises", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            CreateIndex("dbo.Exercises", "Category_Id");
            CreateIndex("dbo.Exercises", "ApplicationUser_Id");
            CreateIndex("dbo.Exercises", "ApplicationUser_Id1");
            AddForeignKey("dbo.Exercises", "Category_Id", "dbo.Categories", "Id");
            AddForeignKey("dbo.Exercises", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Exercises", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exercises", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Exercises", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Exercises", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Exercises", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.Exercises", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Exercises", new[] { "Category_Id" });
            DropColumn("dbo.Exercises", "ApplicationUser_Id1");
            DropColumn("dbo.Exercises", "ApplicationUser_Id");
            DropColumn("dbo.Exercises", "Category_Id");
            DropTable("dbo.Categories");
        }
    }
}
