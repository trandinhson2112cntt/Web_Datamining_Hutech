namespace Web_Datamining.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KhaoSat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KhaoSat",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CMND = c.String(nullable: false, maxLength: 15),
                        Khoi = c.String(maxLength: 5),
                        DiemMon1 = c.Decimal(precision: 18, scale: 2),
                        DiemMon2 = c.Decimal(precision: 18, scale: 2),
                        DiemMon3 = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KhaoSat");
        }
    }
}
