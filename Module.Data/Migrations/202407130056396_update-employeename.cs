namespace Module.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateemployeename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppContactSupports", "SupportEmployeeName", c => c.String());
            DropColumn("dbo.AppContactSupports", "SupportEmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppContactSupports", "SupportEmployeeId", c => c.Int());
            DropColumn("dbo.AppContactSupports", "SupportEmployeeName");
        }
    }
}
