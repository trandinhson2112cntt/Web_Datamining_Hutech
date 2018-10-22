using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface ILuatRepository : IRepository<Luat>
    {
    }

    public class LuatRepository : RepositoryBase<Luat>, ILuatRepository
    {
        public LuatRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}