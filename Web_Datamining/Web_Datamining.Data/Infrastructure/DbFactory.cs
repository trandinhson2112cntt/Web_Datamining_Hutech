namespace Web_Datamining.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private WebDbContext dbContext;

        public WebDbContext Init()
        {
            return dbContext ?? (dbContext = new WebDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}