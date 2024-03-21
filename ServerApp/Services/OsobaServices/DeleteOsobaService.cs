using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ServerApp.Models;

namespace ServerApp.Services.OsobaServices
{
    public class DeleteOsobaService : OsobaInfoService
    {
        public DeleteOsobaService(DBContext context) : base(context)
        {
        }

        public Osoba Method()
        {
            _osoba.Prebivaliste ??= new Mesto();
            dbContext.Database.SqlQuery<Osoba>($"exec sp_osoba_delete @o={_osoba.O}, @prebivaliste={_osoba.Prebivaliste.M}").IgnoreQueryFilters();
            return _osoba;
        }
    }
}
