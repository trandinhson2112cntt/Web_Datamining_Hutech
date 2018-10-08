using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface IHeDaoTaoRepository : IRepository<HeDaoTao>
    {
    }

    public class HeDaoTaoRepository : RepositoryBase<HeDaoTao>, IHeDaoTaoRepository
    {
        public HeDaoTaoRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}