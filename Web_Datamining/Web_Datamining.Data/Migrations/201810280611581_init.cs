namespace Web_Datamining.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DSNguyenVong", "DiemTong", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DSNguyenVong", "DiemTong");
        }
    }
}
