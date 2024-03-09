using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;

namespace ServerApp.Services.OsobaServices
{
    public class GetAllService : OsobaService
    {
        

        public GetAllService(DBContext dbContext) :base(dbContext) { }
        public List<Osoba> getAll()
        {
            return dbContext.Osobe.ToList();
        }
    }
}
