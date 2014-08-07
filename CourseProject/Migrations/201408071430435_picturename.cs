namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class picturename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pictures", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pictures", "Name");
        }
    }
}
