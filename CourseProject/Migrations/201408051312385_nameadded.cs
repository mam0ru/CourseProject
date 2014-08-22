namespace CourseProject.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class nameadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "Name");
        }
    }
}
