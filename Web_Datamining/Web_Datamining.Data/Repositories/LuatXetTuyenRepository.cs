using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface ILuatXetTuyenRepository : IRepository<LuatXetTuyen>
    {
    }

    public class LuatXetTuyenRepository : RepositoryBase<LuatXetTuyen>, ILuatXetTuyenRepository
    {
        public LuatXetTuyenRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}