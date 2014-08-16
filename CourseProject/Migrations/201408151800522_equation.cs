namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class equation : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Formulae", newName: "Equations");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Equations", newName: "Formulae");
        }
    }
}
