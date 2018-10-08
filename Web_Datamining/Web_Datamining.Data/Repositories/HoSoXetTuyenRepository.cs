using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface IHoSoXetTuyenRepository : IRepository<HoSoXetTuyen>
    {
    }

    public class HoSoXetTuyenRepository : RepositoryBase<HoSoXetTuyen>, IHoSoXetTuyenRepository
    {
        public HoSoXetTuyenRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}