using ServerApp.Models;
using ServerApp.Services.OsobaServices;

namespace ServerApp.Services
{
    public class OsobaService : Service
    {
        public OsobaService(DBContext context) : base(context) { }

       /* public List<Osoba> GetAll()
        {
            return new GetAllService(dbContext).GetAll();
        }*/

        public Osoba Insert(Osoba o)
        {
            InsertOsobaService s = new(dbContext) { Osoba = o };
            return s.Method();
        }
        /*public Osoba Update(Osoba o)
        {
            UpdateOsobaService s = new(dbContext) { Osoba = o };
            //return s.Execute();
            return o;
        }
        public String Delete(Osoba o)
        {
            DeleteOsobaService s = new(dbContext) { Osoba = o };
            //s.Execute();
            //return $"User with id {s.Osoba.O} is successfully deleted.";
            return "";

        }*/
    }
}
