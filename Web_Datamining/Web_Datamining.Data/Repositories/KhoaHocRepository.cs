using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface IKhoaHocRepository : IRepository<KhoaHoc>
    {
    }

    public class KhoaHocRepository : RepositoryBase<KhoaHoc>, IKhoaHocRepository
    {
        public KhoaHocRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}