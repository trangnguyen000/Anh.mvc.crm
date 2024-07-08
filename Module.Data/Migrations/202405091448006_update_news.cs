namespace Module.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_news : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppTinTuc", "TypePage", c => c.Int());
            DropColumn("dbo.AppTinTuc", "IsPage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppTinTuc", "IsPage", c => c.Boolean());
            DropColumn("dbo.AppTinTuc", "TypePage");
        }
    }
}
