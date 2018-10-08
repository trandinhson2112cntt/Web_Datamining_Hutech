using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface IDiemHocKyRepository : IRepository<DiemHocKy>
    {
    }

    public class DiemHocKyRepository : RepositoryBase<DiemHocKy>, IDiemHocKyRepository
    {
        public DiemHocKyRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}