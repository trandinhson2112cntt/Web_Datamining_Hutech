using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface IHocKyRepository : IRepository<HocKy>
    {
    }

    public class HocKyRepository : RepositoryBase<HocKy>, IHocKyRepository
    {
        public HocKyRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}