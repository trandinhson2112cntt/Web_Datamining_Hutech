using System;

namespace Web_Datamining.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        WebDbContext Init();
    }
}