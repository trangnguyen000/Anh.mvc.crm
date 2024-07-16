namespace Module.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatestudyname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppContactSupports", "StudyProgramName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppContactSupports", "StudyProgramName");
        }
    }
}
