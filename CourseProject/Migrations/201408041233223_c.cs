namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "Task_Id", "dbo.Exercises");
            DropIndex("dbo.Tags", new[] { "Task_Id" });
            DropColumn("dbo.Tags", "Task_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "Task_Id", c => c.Int());
            CreateIndex("dbo.Tags", "Task_Id");
            AddForeignKey("dbo.Tags", "Task_Id", "dbo.Exercises", "Id");
        }
    }
}
