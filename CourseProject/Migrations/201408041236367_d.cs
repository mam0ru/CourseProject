namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TagExercises",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Exercise_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Exercise_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Exercises", t => t.Exercise_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Exercise_Id);
            
            AddColumn("dbo.AspNetUsers", "Exercise_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Exercise_Id");
            AddForeignKey("dbo.AspNetUsers", "Exercise_Id", "dbo.Exercises", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagExercises", "Exercise_Id", "dbo.Exercises");
            DropForeignKey("dbo.TagExercises", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.AspNetUsers", "Exercise_Id", "dbo.Exercises");
            DropIndex("dbo.TagExercises", new[] { "Exercise_Id" });
            DropIndex("dbo.TagExercises", new[] { "Tag_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Exercise_Id" });
            DropColumn("dbo.AspNetUsers", "Exercise_Id");
            DropTable("dbo.TagExercises");
        }
    }
}
