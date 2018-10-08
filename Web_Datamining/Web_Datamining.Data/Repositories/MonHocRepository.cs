using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface IMonHocRepository : IRepository<MonHoc>
    {
    }

    public class MonHocRepository : RepositoryBase<MonHoc>, IMonHocRepository
    {
        public MonHocRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}