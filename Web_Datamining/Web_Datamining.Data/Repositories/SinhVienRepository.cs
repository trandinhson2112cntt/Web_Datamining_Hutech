using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface ISinhVienBoRepository : IRepository<SinhVien>
    {
    }

    public class SinhVienRepository : RepositoryBase<SinhVien>, ISinhVienBoRepository
    {
        public SinhVienRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}