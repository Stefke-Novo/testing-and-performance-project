using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;

namespace ServerApp.Services
{
    public abstract class Transaction<T> : Service
    {
        protected Transaction(DBContext context) : base(context)
        {
        }

        public T Execute()
        {
            using var transaciton = dbContext.Database.BeginTransaction();
            try
            {
                T result = Method(transaciton);
                transaciton.Commit();
                return result;
            }
            catch (Exception)
            {
                transaciton.Rollback();
                throw;
            }
        }

        public abstract T Method(IDbContextTransaction transaction);
    }
}
