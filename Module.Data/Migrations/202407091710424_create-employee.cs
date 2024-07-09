namespace Module.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createemployee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppContactSupports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(maxLength: 255),
                        EmailAddress = c.String(maxLength: 255),
                        PhoneNumber = c.String(maxLength: 25),
                        Description = c.String(maxLength: 1000),
                        PositionId = c.Int(),
                        TypeProgram = c.Short(),
                        SupportEmployeeId = c.Int(),
                        Note = c.String(maxLength: 500),
                        Status = c.Short(),
                        LastModificationTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastModifierUserId = c.Int(),
                        CreationTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatorUserId = c.Int(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.AppEmployees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(maxLength: 255),
                        EmailAddress = c.String(maxLength: 255),
                        Image = c.String(maxLength: 500),
                        PlaceOfOrigin = c.String(maxLength: 500),
                        DateOfBirth = c.DateTime(),
                        Sex = c.Short(),
                        PhoneNumber = c.String(maxLength: 25),
                        PositionId = c.Int(),
                        Note = c.String(maxLength: 500),
                        Status = c.Short(),
                        LastModificationTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastModifierUserId = c.Int(),
                        CreationTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatorUserId = c.Int(),
                        IsDeleted = c.Boolean(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.AppTinTuc");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AppTinTuc",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TieuDe = c.String(maxLength: 250),
                        Mota = c.String(maxLength: 500),
                        NoiDung = c.String(),
                        Link = c.String(maxLength: 250),
                        Anh = c.String(maxLength: 500),
                        ChuyenMucId = c.Int(),
                        TypePage = c.Int(),
                        TrangThai = c.Byte(),
                        IsViewHome = c.Boolean(),
                        CreationTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatorUserId = c.Long(),
                        LastModificationTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastModifierUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        DeleterUserId = c.Long(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.AppEmployees");
            DropTable("dbo.AppEmployeeExperiences");
            DropTable("dbo.AppContactSupports");
        }
    }
}
