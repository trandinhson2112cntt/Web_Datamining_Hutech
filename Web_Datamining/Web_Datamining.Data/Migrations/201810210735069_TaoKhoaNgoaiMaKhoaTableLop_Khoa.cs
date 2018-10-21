namespace Web_Datamining.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaoKhoaNgoaiMaKhoaTableLop_Khoa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MonHoc", "MaKhoa", c => c.String(maxLength: 10));
            AddColumn("dbo.Lop", "MaKhoa", c => c.String(maxLength: 10));
            CreateIndex("dbo.MonHoc", "MaKhoa");
            CreateIndex("dbo.Lop", "MaKhoa");
            AddForeignKey("dbo.Lop", "MaKhoa", "dbo.Khoa", "MaKhoa");
            AddForeignKey("dbo.MonHoc", "MaKhoa", "dbo.Khoa", "MaKhoa");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MonHoc", "MaKhoa", "dbo.Khoa");
            DropForeignKey("dbo.Lop", "MaKhoa", "dbo.Khoa");
            DropIndex("dbo.Lop", new[] { "MaKhoa" });
            DropIndex("dbo.MonHoc", new[] { "MaKhoa" });
            DropColumn("dbo.Lop", "MaKhoa");
            DropColumn("dbo.MonHoc", "MaKhoa");
        }
    }
}
