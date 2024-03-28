using ServerApp.Models;
using ServerApp.Services.MestoServices;

namespace ServerApp.Services
{

    public class MestoService : Service
    {
        private readonly GetAllService m_allService;

        public MestoService(DBContext context) : base(context)
        {
            this.m_allService = new GetAllService(context);
        }

        public List<Mesto> GetAll()
        {
            return this.m_allService.GetAll();
        }
    }
}
