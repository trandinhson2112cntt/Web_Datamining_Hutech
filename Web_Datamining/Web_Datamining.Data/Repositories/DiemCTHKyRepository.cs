using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface IDiemCTHKyRepository : IRepository<DiemCTHKy>
    {
    }

    public class DiemCTHKyRepository : RepositoryBase<DiemCTHKy>, IDiemCTHKyRepository
    {
        public DiemCTHKyRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}