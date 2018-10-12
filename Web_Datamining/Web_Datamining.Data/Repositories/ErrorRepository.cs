using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Datamining.Data.Infrastructure;
using Web_Datamining.Models;

namespace Web_Datamining.Data.Repositories
{
    public interface IErrorRepository : IRepository<Error>
    {
    }
    public class ErrorRepository : RepositoryBase<Error>, IErrorRepository
    {
        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
