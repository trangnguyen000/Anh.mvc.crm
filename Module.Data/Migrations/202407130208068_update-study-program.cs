namespace Module.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatestudyprogram : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppContactSupports", "FromDataSourceId", c => c.Int());
            AddColumn("dbo.AppContactSupports", "StudyProgramId", c => c.Int());
            DropColumn("dbo.AppContactSupports", "PositionId");
            DropColumn("dbo.AppContactSupports", "TypeProgram");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppContactSupports", "TypeProgram", c => c.Short());
            AddColumn("dbo.AppContactSupports", "PositionId", c => c.Int());
            DropColumn("dbo.AppContactSupports", "StudyProgramId");
            DropColumn("dbo.AppContactSupports", "FromDataSourceId");
        }
    }
}
