using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface INganhTheoBoRepository : IRepository<NganhTheoBo>
    {
    }

    public class NganhTheoBoRepository : RepositoryBase<NganhTheoBo>, INganhTheoBoRepository
    {
        public NganhTheoBoRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}