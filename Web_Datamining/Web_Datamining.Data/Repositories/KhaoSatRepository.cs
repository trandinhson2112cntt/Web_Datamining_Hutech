using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Model.Models;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface IKhaoSatRepository : IRepository<KhaoSat>
    {

    }

    public class KhaoSatRepository : RepositoryBase<KhaoSat>, IKhaoSatRepository
    {
        public KhaoSatRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }


    }
}
