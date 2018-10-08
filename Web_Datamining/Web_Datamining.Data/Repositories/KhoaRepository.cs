using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface IKhoaRepository : IRepository<Khoa>
    {
    }

    public class KhoaRepository : RepositoryBase<Khoa>, IKhoaRepository
    {
        public KhoaRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}