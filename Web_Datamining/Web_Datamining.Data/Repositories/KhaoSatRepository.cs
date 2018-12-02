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
<<<<<<< HEAD
        
=======
>>>>>>> parent of 2fd01a8... Revert "cap nhat bang Khao sao va Api Them Khao Sat"
    }

    public class KhaoSatRepository : RepositoryBase<KhaoSat>, IKhaoSatRepository
    {
        public KhaoSatRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
<<<<<<< HEAD

       
=======
>>>>>>> parent of 2fd01a8... Revert "cap nhat bang Khao sao va Api Them Khao Sat"
    }
}
