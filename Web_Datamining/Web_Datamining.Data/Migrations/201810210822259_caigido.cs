namespace Web_Datamining.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class caigido : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoaiLuat",
                c => new
                    {
                        LuatId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.LuatId);
            
            AddColumn("dbo.Luat", "LuatId", c => c.Int());
            CreateIndex("dbo.Luat", "LuatId");
            AddForeignKey("dbo.Luat", "LuatId", "dbo.LoaiLuat", "LuatId");
            DropTable("dbo.LuatXetTuyen");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LuatXetTuyen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        X = c.String(maxLength: 100),
                        Y = c.String(maxLength: 100),
                        Support = c.Decimal(precision: 18, scale: 2),
                        Confidence = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Luat", "LuatId", "dbo.LoaiLuat");
            DropIndex("dbo.Luat", new[] { "LuatId" });
            DropColumn("dbo.Luat", "LuatId");
            DropTable("dbo.LoaiLuat");
        }
    }
}
