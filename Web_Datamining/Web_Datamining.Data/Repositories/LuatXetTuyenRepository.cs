using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface ILoaiLuatnRepository : IRepository<LoaiLuat>
    {
    }

    public class LoaiLuatRepository : RepositoryBase<LoaiLuat>, ILoaiLuatnRepository
    {
        public LoaiLuatRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}