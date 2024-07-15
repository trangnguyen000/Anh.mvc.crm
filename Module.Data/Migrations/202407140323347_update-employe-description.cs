namespace Module.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateemployedescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppEmployees", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppEmployees", "Description");
        }
    }
}
