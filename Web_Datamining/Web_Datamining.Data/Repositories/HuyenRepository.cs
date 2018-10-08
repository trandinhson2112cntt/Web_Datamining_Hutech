using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface IHuyenRepository : IRepository<Huyen>
    {
    }

    public class HuyenRepository : RepositoryBase<Huyen>, IHuyenRepository
    {
        public HuyenRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}