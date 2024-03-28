using ServerApp.Models;

namespace ServerApp.Services.MestoServices
{
    public class GetAllService : Service
    {
        public GetAllService(DBContext context) : base(context)
        {
        }

        public List<Mesto> GetAll()
        {
            return [.. dbContext.Mesta];
        }
    }
}
