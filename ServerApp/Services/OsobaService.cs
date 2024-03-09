using ServerApp.Models;
using ServerApp.Services.OsobaServices;

namespace ServerApp.Services
{
    public class OsobaService : Service
    {
        
        public OsobaService(DBContext context) : base(context) { }

        public List<Osoba> GetAll()
        {
            return new GetAllService(dbContext).getAll();
        }

        public Osoba Insert(Osoba o)
        {
            InsertOsobaService s = new(dbContext){ Osoba = o };
            return s.Execute();
        }
    }
}
