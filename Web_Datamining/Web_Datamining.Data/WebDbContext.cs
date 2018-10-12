using System.Data.Entity;
using Web_Datamining.Models;

namespace Web_Datamining.Data
{
    public class WebDbContext : DbContext
    {
        public WebDbContext() : base("WebDataminingConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<ChuyenNganh> ChuyenNganhs { get; set; }
        public DbSet<DiemCTHKy> DiemCTHKys { get; set; }
        public DbSet<DiemHocKy> DiemHocKys { get; set; }
        public DbSet<DiemXetTuyen> DiemXetTuyens { get; set; }
        public DbSet<DSNguyenVong> DSNguyenVongs { get; set; }
        public DbSet<HeDaoTao> HeDaoTaos { get; set; }
        public DbSet<HocKy> HocKys { get; set; }
        public DbSet<HoSoXetTuyen> HoSoXetTuyens { get; set; }
        public DbSet<Huyen> Huyens { get; set; }
        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<KhoaHoc> KhoaHocs { get; set; }
        public DbSet<Lop> Lops { get; set; }
        public DbSet<Luat> Luats { get; set; }
        public DbSet<LuatXetTuyen> LuatXetTuyens { get; set; }
        public DbSet<MonHoc> MonHocs { get; set; }
        public DbSet<NganhTheoBo> NganhTheoBos { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<Tinh> Tinhs { get; set; }
        public DbSet<ToHopMon> ToHopMons { get; set; }
        public DbSet<TruongTHPT> TruongTHPTs { get; set; }
        public DbSet<Error> Errors { get; set; }

        public static WebDbContext Create()
        {
            return new WebDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}