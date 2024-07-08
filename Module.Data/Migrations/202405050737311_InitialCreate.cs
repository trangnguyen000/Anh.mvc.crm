namespace Module.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AbpPermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Discriminator = c.String(),
                        Name = c.String(maxLength: 128),
                        RoleId = c.Int(),
                        UserId = c.Int(),
                        CreationTime = c.DateTime(),
                        CreatorUserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        IsDefault = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        Name = c.String(maxLength: 150),
                        NormalizedName = c.String(maxLength: 150),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Int(),
                        CreationTime = c.DateTime(),
                        CreatorUserId = c.Int(),
                        DeleterUserId = c.Int(),
                        DeletionTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpUserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(),
                        UserId = c.Int(),
                        CreationTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatorUserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(maxLength: 250),
                        FirstName = c.String(maxLength: 250),
                        FullName = c.String(maxLength: 250),
                        EmailAddress = c.String(maxLength: 255),
                        IsActive = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        IsEmailConfirmed = c.Boolean(),
                        Password = c.String(maxLength: 128),
                        PasswordResetCode = c.String(maxLength: 128),
                        PhoneNumber = c.String(maxLength: 32),
                        UserName = c.String(maxLength: 256),
                        Age = c.Int(),
                        DonVi = c.String(maxLength: 500),
                        DateOfBirth = c.DateTime(),
                        LastModificationTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastModifierUserId = c.Int(),
                        CreationTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatorUserId = c.Int(),
                        DeleterUserId = c.Int(),
                        DeletionTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppDictionary",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValueId = c.Int(),
                        ValueKey = c.String(maxLength: 150, unicode: false),
                        DisplayName = c.String(maxLength: 250),
                        GroupCode = c.String(maxLength: 150),
                        GhiChu = c.String(maxLength: 500),
                        CreationTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatorUserId = c.Long(),
                        LastModificationTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastModifierUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        DeleterUserId = c.Long(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        TrangThai = c.Byte(),
                        IsPage = c.Boolean(),
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AppTinTuc");
            DropTable("dbo.AppDictionary");
            DropTable("dbo.AbpUsers");
            DropTable("dbo.AbpUserRoles");
            DropTable("dbo.AbpRoles");
            DropTable("dbo.AbpPermissions");
        }
    }
}
