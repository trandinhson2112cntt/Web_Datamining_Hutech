namespace Web_Datamining.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChuyenNganh",
                c => new
                {
                    MaChuyenNganh = c.String(nullable: false, maxLength: 10),
                    TenChuyenNganh = c.String(maxLength: 50),
                    MaKhoa = c.String(nullable: false, maxLength: 10),
                })
                .PrimaryKey(t => t.MaChuyenNganh)
                .ForeignKey("dbo.Khoa", t => t.MaKhoa, cascadeDelete: true)
                .Index(t => t.MaKhoa);

            CreateTable(
                "dbo.Khoa",
                c => new
                {
                    MaKhoa = c.String(nullable: false, maxLength: 10),
                    TenKhoa = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.MaKhoa);

            CreateTable(
                "dbo.Lop",
                c => new
                {
                    ID_Lop = c.Int(nullable: false),
                    MaChuyenNganh = c.String(nullable: false, maxLength: 10),
                    MaHeDaoTao = c.String(nullable: false, maxLength: 10),
                    MaKhoaHoc = c.Int(nullable: false),
                    TenLop = c.String(maxLength: 10),
                })
                .PrimaryKey(t => t.ID_Lop)
                .ForeignKey("dbo.ChuyenNganh", t => t.MaChuyenNganh, cascadeDelete: true)
                .ForeignKey("dbo.HeDaoTao", t => t.MaHeDaoTao, cascadeDelete: true)
                .ForeignKey("dbo.KhoaHoc", t => t.MaKhoaHoc, cascadeDelete: true)
                .Index(t => t.MaChuyenNganh)
                .Index(t => t.MaHeDaoTao)
                .Index(t => t.MaKhoaHoc);

            CreateTable(
                "dbo.HeDaoTao",
                c => new
                {
                    MaHeDaoTao = c.String(nullable: false, maxLength: 10),
                    TenHeDaoTao = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.MaHeDaoTao);

            CreateTable(
                "dbo.KhoaHoc",
                c => new
                {
                    MaKhoaHoc = c.Int(nullable: false),
                    NamHoc = c.String(maxLength: 10),
                })
                .PrimaryKey(t => t.MaKhoaHoc);

            CreateTable(
                "dbo.SinhVien",
                c => new
                {
                    MSSV = c.String(nullable: false, maxLength: 15),
                    MaHoSo = c.Int(nullable: false),
                    CoVanHocTap = c.String(storeType: "ntext"),
                    ID_Lop = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.MSSV)
                .ForeignKey("dbo.HoSoXetTuyen", t => t.MaHoSo, cascadeDelete: true)
                .ForeignKey("dbo.Lop", t => t.ID_Lop, cascadeDelete: true)
                .Index(t => t.MaHoSo)
                .Index(t => t.ID_Lop);

            CreateTable(
                "dbo.DiemHocKy",
                c => new
                {
                    MSSV = c.String(nullable: false, maxLength: 15),
                    ID_HocKi = c.Int(nullable: false),
                    SoTCDK = c.Int(),
                    SoTCD = c.Int(),
                    SoTCTL = c.Int(),
                    DiemTBTLHe4 = c.Double(),
                })
                .PrimaryKey(t => new { t.MSSV, t.ID_HocKi })
                .ForeignKey("dbo.HocKy", t => t.ID_HocKi, cascadeDelete: true)
                .ForeignKey("dbo.SinhVien", t => t.MSSV, cascadeDelete: true)
                .Index(t => t.MSSV)
                .Index(t => t.ID_HocKi);

            CreateTable(
                "dbo.DiemCTHKy",
                c => new
                {
                    MaMon = c.String(nullable: false, maxLength: 10),
                    MSSV = c.String(nullable: false, maxLength: 15),
                    ID_HocKi = c.Int(nullable: false),
                    DiemTH = c.Double(),
                    DiemQT = c.Double(),
                    DiemThi1 = c.Double(),
                    DiemThi2 = c.Double(),
                    TiLeDiemTH = c.Double(),
                    TiLeDiemQT = c.Double(),
                    TiLeDiemThi1 = c.Double(),
                    TiLeDiemThi2 = c.Double(),
                    DiemTKHe10 = c.Double(),
                    DiemTKHe4 = c.Double(),
                    DiemTKChu = c.String(maxLength: 50),
                })
                .PrimaryKey(t => new { t.MaMon, t.MSSV, t.ID_HocKi })
                .ForeignKey("dbo.DiemHocKy", t => new { t.MSSV, t.ID_HocKi }, cascadeDelete: true)
                .ForeignKey("dbo.MonHoc", t => t.MaMon, cascadeDelete: true)
                .Index(t => t.MaMon)
                .Index(t => new { t.MSSV, t.ID_HocKi });

            CreateTable(
                "dbo.MonHoc",
                c => new
                {
                    MaMon = c.String(nullable: false, maxLength: 10),
                    TenMon = c.String(maxLength: 50),
                    TichLuy = c.Boolean(),
                    DiemDat = c.Double(),
                })
                .PrimaryKey(t => t.MaMon);

            CreateTable(
                "dbo.HocKy",
                c => new
                {
                    ID_HocKi = c.Int(nullable: false),
                    NamHoc = c.String(maxLength: 10),
                    KyHoc = c.Int(),
                })
                .PrimaryKey(t => t.ID_HocKi);

            CreateTable(
                "dbo.HoSoXetTuyen",
                c => new
                {
                    MaHoSo = c.Int(nullable: false),
                    MaTruongTHPT = c.Int(nullable: false),
                    CMDN = c.String(maxLength: 15),
                    NgaySinh = c.DateTime(storeType: "smalldatetime"),
                    HoTen = c.String(maxLength: 50),
                    GioiTinh = c.Int(),
                    DanToc = c.String(maxLength: 30),
                    TinhTrangTrungTuyen = c.Int(),
                    DXT_ID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.MaHoSo)
                .ForeignKey("dbo.DiemXetTuyen", t => t.DXT_ID, cascadeDelete: true)
                .ForeignKey("dbo.TruongTHPT", t => t.MaTruongTHPT, cascadeDelete: true)
                .Index(t => t.MaTruongTHPT)
                .Index(t => t.DXT_ID);

            CreateTable(
                "dbo.DiemXetTuyen",
                c => new
                {
                    DXT_ID = c.Int(nullable: false, identity: true),
                    DiemToan = c.Double(),
                    DiemVan = c.Double(),
                    DiemLy = c.Double(),
                    DiemHoa = c.Double(),
                    DiemSinh = c.Double(),
                    DiemDia = c.Double(),
                    DiemGDCD = c.Double(),
                    DiemNN = c.Double(),
                    HinhThucXetTuyen = c.Boolean(),
                })
                .PrimaryKey(t => t.DXT_ID);

            CreateTable(
                "dbo.DSNguyenVong",
                c => new
                {
                    MaHoSo = c.Int(nullable: false),
                    ThuTuNV = c.Int(nullable: false),
                    MaToHop = c.String(nullable: false, maxLength: 5),
                    MaNganh = c.String(nullable: false, maxLength: 50),
                    MaTDH = c.String(maxLength: 10),
                    TrangThaiNV = c.String(maxLength: 10),
                })
                .PrimaryKey(t => new { t.MaHoSo, t.ThuTuNV })
                .ForeignKey("dbo.HoSoXetTuyen", t => t.MaHoSo, cascadeDelete: true)
                .ForeignKey("dbo.NganhTheoBo", t => t.MaNganh, cascadeDelete: true)
                .ForeignKey("dbo.ToHopMon", t => t.MaToHop, cascadeDelete: true)
                .Index(t => t.MaHoSo)
                .Index(t => t.MaToHop)
                .Index(t => t.MaNganh);

            CreateTable(
                "dbo.NganhTheoBo",
                c => new
                {
                    MaNganh = c.String(nullable: false, maxLength: 50),
                    TeNganh = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.MaNganh);

            CreateTable(
                "dbo.ToHopMon",
                c => new
                {
                    MaToHop = c.String(nullable: false, maxLength: 5),
                    Mon1 = c.String(maxLength: 20),
                    Mon2 = c.String(maxLength: 20),
                    Mon3 = c.String(maxLength: 20),
                })
                .PrimaryKey(t => t.MaToHop);

            CreateTable(
                "dbo.TruongTHPT",
                c => new
                {
                    MaTruongTHPT = c.Int(nullable: false),
                    MaHuyen = c.Int(nullable: false),
                    MaTinh = c.Int(nullable: false),
                    TenTruong = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.MaTruongTHPT)
                .ForeignKey("dbo.Huyen", t => new { t.MaHuyen, t.MaTinh }, cascadeDelete: true)
                .Index(t => new { t.MaHuyen, t.MaTinh });

            CreateTable(
                "dbo.Huyen",
                c => new
                {
                    MaHuyen = c.Int(nullable: false),
                    MaTinh = c.Int(nullable: false),
                    khuvuc = c.String(maxLength: 20),
                    TenHuyen = c.String(maxLength: 50),
                })
                .PrimaryKey(t => new { t.MaHuyen, t.MaTinh })
                .ForeignKey("dbo.Tinh", t => t.MaTinh, cascadeDelete: true)
                .Index(t => t.MaTinh);

            CreateTable(
                "dbo.Tinh",
                c => new
                {
                    MaTinh = c.Int(nullable: false),
                    TenTinh = c.String(maxLength: 50),
                    KhuVuc = c.String(maxLength: 20),
                })
                .PrimaryKey(t => t.MaTinh);

            CreateTable(
                "dbo.Luat",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    X = c.String(maxLength: 100),
                    Y = c.String(maxLength: 100),
                    Support = c.Decimal(precision: 18, scale: 2),
                    Confidence = c.Decimal(precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.Id);

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
        }

        public override void Down()
        {
            DropForeignKey("dbo.SinhVien", "ID_Lop", "dbo.Lop");
            DropForeignKey("dbo.TruongTHPT", new[] { "MaHuyen", "MaTinh" }, "dbo.Huyen");
            DropForeignKey("dbo.Huyen", "MaTinh", "dbo.Tinh");
            DropForeignKey("dbo.HoSoXetTuyen", "MaTruongTHPT", "dbo.TruongTHPT");
            DropForeignKey("dbo.SinhVien", "MaHoSo", "dbo.HoSoXetTuyen");
            DropForeignKey("dbo.DSNguyenVong", "MaToHop", "dbo.ToHopMon");
            DropForeignKey("dbo.DSNguyenVong", "MaNganh", "dbo.NganhTheoBo");
            DropForeignKey("dbo.DSNguyenVong", "MaHoSo", "dbo.HoSoXetTuyen");
            DropForeignKey("dbo.HoSoXetTuyen", "DXT_ID", "dbo.DiemXetTuyen");
            DropForeignKey("dbo.DiemHocKy", "MSSV", "dbo.SinhVien");
            DropForeignKey("dbo.DiemHocKy", "ID_HocKi", "dbo.HocKy");
            DropForeignKey("dbo.DiemCTHKy", "MaMon", "dbo.MonHoc");
            DropForeignKey("dbo.DiemCTHKy", new[] { "MSSV", "ID_HocKi" }, "dbo.DiemHocKy");
            DropForeignKey("dbo.Lop", "MaKhoaHoc", "dbo.KhoaHoc");
            DropForeignKey("dbo.Lop", "MaHeDaoTao", "dbo.HeDaoTao");
            DropForeignKey("dbo.Lop", "MaChuyenNganh", "dbo.ChuyenNganh");
            DropForeignKey("dbo.ChuyenNganh", "MaKhoa", "dbo.Khoa");
            DropIndex("dbo.Huyen", new[] { "MaTinh" });
            DropIndex("dbo.TruongTHPT", new[] { "MaHuyen", "MaTinh" });
            DropIndex("dbo.DSNguyenVong", new[] { "MaNganh" });
            DropIndex("dbo.DSNguyenVong", new[] { "MaToHop" });
            DropIndex("dbo.DSNguyenVong", new[] { "MaHoSo" });
            DropIndex("dbo.HoSoXetTuyen", new[] { "DXT_ID" });
            DropIndex("dbo.HoSoXetTuyen", new[] { "MaTruongTHPT" });
            DropIndex("dbo.DiemCTHKy", new[] { "MSSV", "ID_HocKi" });
            DropIndex("dbo.DiemCTHKy", new[] { "MaMon" });
            DropIndex("dbo.DiemHocKy", new[] { "ID_HocKi" });
            DropIndex("dbo.DiemHocKy", new[] { "MSSV" });
            DropIndex("dbo.SinhVien", new[] { "ID_Lop" });
            DropIndex("dbo.SinhVien", new[] { "MaHoSo" });
            DropIndex("dbo.Lop", new[] { "MaKhoaHoc" });
            DropIndex("dbo.Lop", new[] { "MaHeDaoTao" });
            DropIndex("dbo.Lop", new[] { "MaChuyenNganh" });
            DropIndex("dbo.ChuyenNganh", new[] { "MaKhoa" });
            DropTable("dbo.LuatXetTuyen");
            DropTable("dbo.Luat");
            DropTable("dbo.Tinh");
            DropTable("dbo.Huyen");
            DropTable("dbo.TruongTHPT");
            DropTable("dbo.ToHopMon");
            DropTable("dbo.NganhTheoBo");
            DropTable("dbo.DSNguyenVong");
            DropTable("dbo.DiemXetTuyen");
            DropTable("dbo.HoSoXetTuyen");
            DropTable("dbo.HocKy");
            DropTable("dbo.MonHoc");
            DropTable("dbo.DiemCTHKy");
            DropTable("dbo.DiemHocKy");
            DropTable("dbo.SinhVien");
            DropTable("dbo.KhoaHoc");
            DropTable("dbo.HeDaoTao");
            DropTable("dbo.Lop");
            DropTable("dbo.Khoa");
            DropTable("dbo.ChuyenNganh");
        }
    }
}