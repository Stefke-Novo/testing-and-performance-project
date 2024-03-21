using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using ServerApp.Models;

namespace ServerApp.Services.OsobaServices
{
    public class GetAllService : OsobaInfoService
    {
        

        public GetAllService(DBContext dbContext) :base(dbContext) { }
        public List<Osoba> GetAll()
        {
            return dbContext.Osobe.ToList();
        }

        /*public override Osoba Method(IDbContextTransaction transaction)
        {
            throw new NotImplementedException();
        }*/
    }
}
