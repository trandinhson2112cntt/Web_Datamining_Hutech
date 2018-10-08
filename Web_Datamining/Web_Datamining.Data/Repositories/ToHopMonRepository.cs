using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface IToHopMonRepository : IRepository<ToHopMon>
    {
    }

    public class ToHopMonRepository : RepositoryBase<ToHopMon>, IToHopMonRepository
    {
        public ToHopMonRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}