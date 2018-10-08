using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface IDiemXetTuyenRepository : IRepository<DiemXetTuyen>
    {
    }

    public class DiemXetTuyenRepository : RepositoryBase<DiemXetTuyen>, IDiemXetTuyenRepository
    {
        public DiemXetTuyenRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}