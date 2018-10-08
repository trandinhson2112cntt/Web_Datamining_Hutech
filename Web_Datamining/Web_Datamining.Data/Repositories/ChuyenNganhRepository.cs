using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface IChuyenNganhRepository : IRepository<ChuyenNganh>
    {
    }

    public class ChuyenNganRepository : RepositoryBase<ChuyenNganh>, IChuyenNganhRepository
    {
        public ChuyenNganRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}