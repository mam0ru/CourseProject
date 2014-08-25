namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Exercises", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Answers", "Task_Id", "dbo.Exercises");
            DropForeignKey("dbo.Comments", "Target_Id", "dbo.Exercises");
            DropForeignKey("dbo.Equations", "Task_Id", "dbo.Exercises");
            DropForeignKey("dbo.Evaluations", "Target_Id", "dbo.Exercises");
            DropForeignKey("dbo.Graphs", "Task_Id", "dbo.Exercises");
            DropForeignKey("dbo.Pictures", "Task_Id", "dbo.Exercises");
            DropForeignKey("dbo.Videos", "Task_Id", "dbo.Exercises");
            DropIndex("dbo.Exercises", new[] { "Category_Id" });
            DropIndex("dbo.Answers", new[] { "Task_Id" });
            DropIndex("dbo.Comments", new[] { "Target_Id" });
            DropIndex("dbo.Evaluations", new[] { "Target_Id" });
            DropIndex("dbo.Equations", new[] { "Task_Id" });
            DropIndex("dbo.Graphs", new[] { "Task_Id" });
            DropIndex("dbo.Pictures", new[] { "Task_Id" });
            DropIndex("dbo.Videos", new[] { "Task_Id" });
            RenameColumn(table: "dbo.Exercises", name: "Category_Id", newName: "CategoryId");
            RenameColumn(table: "dbo.Answers", name: "Task_Id", newName: "TaskId");
            RenameColumn(table: "dbo.Exercises", name: "Author_Id", newName: "AuthorId");
            RenameColumn(table: "dbo.Comments", name: "Target_Id", newName: "TargetId");
            RenameColumn(table: "dbo.Equations", name: "Task_Id", newName: "TaskId");
            RenameColumn(table: "dbo.Evaluations", name: "Target_Id", newName: "TargetId");
            RenameColumn(table: "dbo.Graphs", name: "Task_Id", newName: "TaskId");
            RenameColumn(table: "dbo.Pictures", name: "Task_Id", newName: "TaskId");
            RenameColumn(table: "dbo.Videos", name: "Task_Id", newName: "TaskId");
            RenameColumn(table: "dbo.Evaluations", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Exercises", name: "IX_Author_Id", newName: "IX_AuthorId");
            RenameIndex(table: "dbo.Evaluations", name: "IX_User_Id", newName: "IX_UserId");
            AlterColumn("dbo.Exercises", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Answers", "TaskId", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "TargetId", c => c.Int(nullable: false));
            AlterColumn("dbo.Evaluations", "TargetId", c => c.Int(nullable: false));
            AlterColumn("dbo.Equations", "TaskId", c => c.Int(nullable: false));
            AlterColumn("dbo.Graphs", "TaskId", c => c.Int(nullable: false));
            AlterColumn("dbo.Pictures", "TaskId", c => c.Int(nullable: false));
            AlterColumn("dbo.Videos", "TaskId", c => c.Int(nullable: false));
            CreateIndex("dbo.Exercises", "CategoryId");
            CreateIndex("dbo.Answers", "TaskId");
            CreateIndex("dbo.Comments", "TargetId");
            CreateIndex("dbo.Evaluations", "TargetId");
            CreateIndex("dbo.Equations", "TaskId");
            CreateIndex("dbo.Graphs", "TaskId");
            CreateIndex("dbo.Pictures", "TaskId");
            CreateIndex("dbo.Videos", "TaskId");
            AddForeignKey("dbo.Exercises", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Answers", "TaskId", "dbo.Exercises", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "TargetId", "dbo.Exercises", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Equations", "TaskId", "dbo.Exercises", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Evaluations", "TargetId", "dbo.Exercises", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Graphs", "TaskId", "dbo.Exercises", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Pictures", "TaskId", "dbo.Exercises", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Videos", "TaskId", "dbo.Exercises", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "TaskId", "dbo.Exercises");
            DropForeignKey("dbo.Pictures", "TaskId", "dbo.Exercises");
            DropForeignKey("dbo.Graphs", "TaskId", "dbo.Exercises");
            DropForeignKey("dbo.Evaluations", "TargetId", "dbo.Exercises");
            DropForeignKey("dbo.Equations", "TaskId", "dbo.Exercises");
            DropForeignKey("dbo.Comments", "TargetId", "dbo.Exercises");
            DropForeignKey("dbo.Answers", "TaskId", "dbo.Exercises");
            DropForeignKey("dbo.Exercises", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Videos", new[] { "TaskId" });
            DropIndex("dbo.Pictures", new[] { "TaskId" });
            DropIndex("dbo.Graphs", new[] { "TaskId" });
            DropIndex("dbo.Equations", new[] { "TaskId" });
            DropIndex("dbo.Evaluations", new[] { "TargetId" });
            DropIndex("dbo.Comments", new[] { "TargetId" });
            DropIndex("dbo.Answers", new[] { "TaskId" });
            DropIndex("dbo.Exercises", new[] { "CategoryId" });
            AlterColumn("dbo.Videos", "TaskId", c => c.Int());
            AlterColumn("dbo.Pictures", "TaskId", c => c.Int());
            AlterColumn("dbo.Graphs", "TaskId", c => c.Int());
            AlterColumn("dbo.Equations", "TaskId", c => c.Int());
            AlterColumn("dbo.Evaluations", "TargetId", c => c.Int());
            AlterColumn("dbo.Comments", "TargetId", c => c.Int());
            AlterColumn("dbo.Answers", "TaskId", c => c.Int());
            AlterColumn("dbo.Exercises", "CategoryId", c => c.Int());
            RenameIndex(table: "dbo.Evaluations", name: "IX_UserId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.Exercises", name: "IX_AuthorId", newName: "IX_Author_Id");
            RenameColumn(table: "dbo.Evaluations", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Videos", name: "TaskId", newName: "Task_Id");
            RenameColumn(table: "dbo.Pictures", name: "TaskId", newName: "Task_Id");
            RenameColumn(table: "dbo.Graphs", name: "TaskId", newName: "Task_Id");
            RenameColumn(table: "dbo.Evaluations", name: "TargetId", newName: "Target_Id");
            RenameColumn(table: "dbo.Equations", name: "TaskId", newName: "Task_Id");
            RenameColumn(table: "dbo.Comments", name: "TargetId", newName: "Target_Id");
            RenameColumn(table: "dbo.Exercises", name: "AuthorId", newName: "Author_Id");
            RenameColumn(table: "dbo.Answers", name: "TaskId", newName: "Task_Id");
            RenameColumn(table: "dbo.Exercises", name: "CategoryId", newName: "Category_Id");
            CreateIndex("dbo.Videos", "Task_Id");
            CreateIndex("dbo.Pictures", "Task_Id");
            CreateIndex("dbo.Graphs", "Task_Id");
            CreateIndex("dbo.Equations", "Task_Id");
            CreateIndex("dbo.Evaluations", "Target_Id");
            CreateIndex("dbo.Comments", "Target_Id");
            CreateIndex("dbo.Answers", "Task_Id");
            CreateIndex("dbo.Exercises", "Category_Id");
            AddForeignKey("dbo.Videos", "Task_Id", "dbo.Exercises", "Id");
            AddForeignKey("dbo.Pictures", "Task_Id", "dbo.Exercises", "Id");
            AddForeignKey("dbo.Graphs", "Task_Id", "dbo.Exercises", "Id");
            AddForeignKey("dbo.Evaluations", "Target_Id", "dbo.Exercises", "Id");
            AddForeignKey("dbo.Equations", "Task_Id", "dbo.Exercises", "Id");
            AddForeignKey("dbo.Comments", "Target_Id", "dbo.Exercises", "Id");
            AddForeignKey("dbo.Answers", "Task_Id", "dbo.Exercises", "Id");
            AddForeignKey("dbo.Exercises", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
