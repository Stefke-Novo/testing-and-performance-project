using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Services.OsobaServices
{
    public class DeleteOsobaService : OsobaInfoService<Osoba>
    {
        public DeleteOsobaService(DBContext context) : base(context)
        {
        }

        public override Osoba Method()
        {
            _osoba.Prebivaliste ??= new Mesto();
            Osoba result = dbContext.Osobe.FromSql($"exec sp_osoba_delete @o={_osoba.O}, @prebivaliste={_osoba.Prebivaliste.M}").AsEnumerable().Single();
            return result;
        }
    }
}
