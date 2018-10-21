namespace Web_Datamining.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableChuongTrinhDaoTao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChungTrinhDaoTao",
                c => new
                    {
                        KhoaID = c.Int(nullable: false),
                        HocKyID = c.Int(nullable: false),
                        MonHocID = c.Int(nullable: false),
                        HocKy_ID_HocKi = c.Int(),
                        Khoa_MaKhoa = c.String(maxLength: 10),
                        MonHoc_MaMon = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => new { t.KhoaID, t.HocKyID, t.MonHocID })
                .ForeignKey("dbo.HocKy", t => t.HocKy_ID_HocKi)
                .ForeignKey("dbo.Khoa", t => t.Khoa_MaKhoa)
                .ForeignKey("dbo.MonHoc", t => t.MonHoc_MaMon)
                .Index(t => t.HocKy_ID_HocKi)
                .Index(t => t.Khoa_MaKhoa)
                .Index(t => t.MonHoc_MaMon);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChungTrinhDaoTao", "MonHoc_MaMon", "dbo.MonHoc");
            DropForeignKey("dbo.ChungTrinhDaoTao", "Khoa_MaKhoa", "dbo.Khoa");
            DropForeignKey("dbo.ChungTrinhDaoTao", "HocKy_ID_HocKi", "dbo.HocKy");
            DropIndex("dbo.ChungTrinhDaoTao", new[] { "MonHoc_MaMon" });
            DropIndex("dbo.ChungTrinhDaoTao", new[] { "Khoa_MaKhoa" });
            DropIndex("dbo.ChungTrinhDaoTao", new[] { "HocKy_ID_HocKi" });
            DropTable("dbo.ChungTrinhDaoTao");
        }
    }
}
