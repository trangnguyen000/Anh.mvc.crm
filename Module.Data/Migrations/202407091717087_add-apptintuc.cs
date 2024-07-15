namespace Module.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addapptintuc : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AppTinTuc");
        }
    }
}
