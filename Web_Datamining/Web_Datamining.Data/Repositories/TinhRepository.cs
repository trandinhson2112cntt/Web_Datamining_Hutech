using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface ITinhRepository : IRepository<Tinh>
    {
    }

    public class TinhRepository : RepositoryBase<Tinh>, ITinhRepository
    {
        public TinhRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}