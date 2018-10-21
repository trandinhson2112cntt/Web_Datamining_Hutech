namespace Web_Datamining.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditTableChuognTrinhDaoTao : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChungTrinhDaoTao", "HocKy_ID_HocKi", "dbo.HocKy");
            DropForeignKey("dbo.ChungTrinhDaoTao", "Khoa_MaKhoa", "dbo.Khoa");
            DropForeignKey("dbo.ChungTrinhDaoTao", "MonHoc_MaMon", "dbo.MonHoc");
            DropIndex("dbo.ChungTrinhDaoTao", new[] { "HocKy_ID_HocKi" });
            DropIndex("dbo.ChungTrinhDaoTao", new[] { "Khoa_MaKhoa" });
            DropIndex("dbo.ChungTrinhDaoTao", new[] { "MonHoc_MaMon" });
            RenameColumn(table: "dbo.ChungTrinhDaoTao", name: "HocKy_ID_HocKi", newName: "ID_HocKi");
            RenameColumn(table: "dbo.ChungTrinhDaoTao", name: "Khoa_MaKhoa", newName: "MaKhoa");
            RenameColumn(table: "dbo.ChungTrinhDaoTao", name: "MonHoc_MaMon", newName: "MaMon");
            DropPrimaryKey("dbo.ChungTrinhDaoTao");
            AlterColumn("dbo.ChungTrinhDaoTao", "ID_HocKi", c => c.Int(nullable: false));
            AlterColumn("dbo.ChungTrinhDaoTao", "MaKhoa", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.ChungTrinhDaoTao", "MaMon", c => c.String(nullable: false, maxLength: 10));
            AddPrimaryKey("dbo.ChungTrinhDaoTao", new[] { "MaKhoa", "ID_HocKi", "MaMon" });
            CreateIndex("dbo.ChungTrinhDaoTao", "MaKhoa");
            CreateIndex("dbo.ChungTrinhDaoTao", "ID_HocKi");
            CreateIndex("dbo.ChungTrinhDaoTao", "MaMon");
            AddForeignKey("dbo.ChungTrinhDaoTao", "ID_HocKi", "dbo.HocKy", "ID_HocKi", cascadeDelete: true);
            AddForeignKey("dbo.ChungTrinhDaoTao", "MaKhoa", "dbo.Khoa", "MaKhoa", cascadeDelete: true);
            AddForeignKey("dbo.ChungTrinhDaoTao", "MaMon", "dbo.MonHoc", "MaMon", cascadeDelete: true);
            DropColumn("dbo.ChungTrinhDaoTao", "KhoaID");
            DropColumn("dbo.ChungTrinhDaoTao", "HocKyID");
            DropColumn("dbo.ChungTrinhDaoTao", "MonHocID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChungTrinhDaoTao", "MonHocID", c => c.Int(nullable: false));
            AddColumn("dbo.ChungTrinhDaoTao", "HocKyID", c => c.Int(nullable: false));
            AddColumn("dbo.ChungTrinhDaoTao", "KhoaID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ChungTrinhDaoTao", "MaMon", "dbo.MonHoc");
            DropForeignKey("dbo.ChungTrinhDaoTao", "MaKhoa", "dbo.Khoa");
            DropForeignKey("dbo.ChungTrinhDaoTao", "ID_HocKi", "dbo.HocKy");
            DropIndex("dbo.ChungTrinhDaoTao", new[] { "MaMon" });
            DropIndex("dbo.ChungTrinhDaoTao", new[] { "ID_HocKi" });
            DropIndex("dbo.ChungTrinhDaoTao", new[] { "MaKhoa" });
            DropPrimaryKey("dbo.ChungTrinhDaoTao");
            AlterColumn("dbo.ChungTrinhDaoTao", "MaMon", c => c.String(maxLength: 10));
            AlterColumn("dbo.ChungTrinhDaoTao", "MaKhoa", c => c.String(maxLength: 10));
            AlterColumn("dbo.ChungTrinhDaoTao", "ID_HocKi", c => c.Int());
            AddPrimaryKey("dbo.ChungTrinhDaoTao", new[] { "KhoaID", "HocKyID", "MonHocID" });
            RenameColumn(table: "dbo.ChungTrinhDaoTao", name: "MaMon", newName: "MonHoc_MaMon");
            RenameColumn(table: "dbo.ChungTrinhDaoTao", name: "MaKhoa", newName: "Khoa_MaKhoa");
            RenameColumn(table: "dbo.ChungTrinhDaoTao", name: "ID_HocKi", newName: "HocKy_ID_HocKi");
            CreateIndex("dbo.ChungTrinhDaoTao", "MonHoc_MaMon");
            CreateIndex("dbo.ChungTrinhDaoTao", "Khoa_MaKhoa");
            CreateIndex("dbo.ChungTrinhDaoTao", "HocKy_ID_HocKi");
            AddForeignKey("dbo.ChungTrinhDaoTao", "MonHoc_MaMon", "dbo.MonHoc", "MaMon");
            AddForeignKey("dbo.ChungTrinhDaoTao", "Khoa_MaKhoa", "dbo.Khoa", "MaKhoa");
            AddForeignKey("dbo.ChungTrinhDaoTao", "HocKy_ID_HocKi", "dbo.HocKy", "ID_HocKi");
        }
    }
}
