namespace Module.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetableemployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppEmployees", "Gender", c => c.Short());
            AlterColumn("dbo.AppEmployees", "Note", c => c.String(maxLength: 2000));
            DropColumn("dbo.AppContactSupports", "StudyProgramId");
            DropColumn("dbo.AppEmployees", "Sex");
            DropColumn("dbo.AppEmployees", "PositionId");
            DropTable("dbo.AppEmployeeExperiences");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AppEmployeeExperiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        PositionId = c.Int(),
                        TypeExperience = c.Short(),
                        EndDateTime = c.DateTime(),
                        StartDateTime = c.DateTime(),
                        Note = c.String(maxLength: 500),
                        LastModificationTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastModifierUserId = c.Int(),
                        CreationTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatorUserId = c.Int(),
                        IsDeleted = c.Boolean(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AppEmployees", "PositionId", c => c.Int());
            AddColumn("dbo.AppEmployees", "Sex", c => c.Short());
            AddColumn("dbo.AppContactSupports", "StudyProgramId", c => c.Int());
            AlterColumn("dbo.AppEmployees", "Note", c => c.String(maxLength: 500));
            DropColumn("dbo.AppEmployees", "Gender");
        }
    }
}
