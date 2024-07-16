namespace Module.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetableemployeeno : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppEmployees", "No", c => c.Int());
            DropColumn("dbo.AppEmployees", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppEmployees", "Status", c => c.Short());
            DropColumn("dbo.AppEmployees", "No");
        }
    }
}
