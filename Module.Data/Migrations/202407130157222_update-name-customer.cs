namespace Module.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatenamecustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppContactSupports", "CustomerName", c => c.String(maxLength: 255));
            DropColumn("dbo.AppContactSupports", "FullName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppContactSupports", "FullName", c => c.String(maxLength: 255));
            DropColumn("dbo.AppContactSupports", "CustomerName");
        }
    }
}
