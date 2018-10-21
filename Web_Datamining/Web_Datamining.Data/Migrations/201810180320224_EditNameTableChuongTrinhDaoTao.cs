namespace Web_Datamining.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditNameTableChuongTrinhDaoTao : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ChungTrinhDaoTao", newName: "ChuongTrinhDaoTao");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ChuongTrinhDaoTao", newName: "ChungTrinhDaoTao");
        }
    }
}
