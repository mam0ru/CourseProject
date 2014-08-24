namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class author : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Exercises", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Exercises", new[] { "AuthorId" });
            AlterColumn("dbo.Exercises", "AuthorId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Exercises", "AuthorId");
            AddForeignKey("dbo.Exercises", "AuthorId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exercises", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Exercises", new[] { "AuthorId" });
            AlterColumn("dbo.Exercises", "AuthorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Exercises", "AuthorId");
            AddForeignKey("dbo.Exercises", "AuthorId", "dbo.AspNetUsers", "Id");
        }
    }
}
