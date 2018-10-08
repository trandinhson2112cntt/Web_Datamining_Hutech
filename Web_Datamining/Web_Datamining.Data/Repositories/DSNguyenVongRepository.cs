using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface IDSNguyenVongRepository : IRepository<DSNguyenVong>
    {
    }

    public class DSNguyenVongRepository : RepositoryBase<DSNguyenVong>, IDSNguyenVongRepository
    {
        public DSNguyenVongRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}