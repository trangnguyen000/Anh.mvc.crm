namespace Module.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetableemployeelanguage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppEmployees", "Language", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppEmployees", "Language");
        }
    }
}
