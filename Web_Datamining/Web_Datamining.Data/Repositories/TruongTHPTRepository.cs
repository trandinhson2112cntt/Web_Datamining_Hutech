using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface ITruongTHPTRepository : IRepository<TruongTHPT>
    {
    }

    public class TruongTHPTRepository : RepositoryBase<TruongTHPT>, ITruongTHPTRepository
    {
        public TruongTHPTRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}