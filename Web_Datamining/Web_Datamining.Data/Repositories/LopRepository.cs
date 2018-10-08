using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface ILopRepository : IRepository<Lop>
    {
    }

    public class LopRepository : RepositoryBase<Lop>, ILopRepository
    {
        public LopRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}