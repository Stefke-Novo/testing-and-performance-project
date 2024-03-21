using ServerApp.Models;

namespace ServerApp.Services
{
    public abstract class Service
    {
        protected readonly DBContext dbContext;
        public Service(DBContext context) =>this.dbContext = context;
    }
}
