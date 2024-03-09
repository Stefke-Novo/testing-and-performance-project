namespace ServerApp.Services
{
    public abstract class Transaction<T> : Service
    {
        protected Transaction(DBContext context) : base(context)
        {
        }

        public T Execute()
        {
            using (var transaciton = dbContext.Database.BeginTransaction())
            {
                /*try
                {*/
                    T result = Method();
                    transaciton.Commit();
                    return result;
                /*}catch (Exception ex) 
                { 
                    transaciton.Rollback();
                    throw ex;
                }*/
            }
        }
        public abstract T Method();
    }
}
