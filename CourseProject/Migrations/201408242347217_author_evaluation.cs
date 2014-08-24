namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class author_evaluation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Evaluations", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Evaluations", new[] { "UserId" });
            AlterColumn("dbo.Evaluations", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Evaluations", "UserId");
            AddForeignKey("dbo.Evaluations", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evaluations", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Evaluations", new[] { "UserId" });
            AlterColumn("dbo.Evaluations", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Evaluations", "UserId");
            AddForeignKey("dbo.Evaluations", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
