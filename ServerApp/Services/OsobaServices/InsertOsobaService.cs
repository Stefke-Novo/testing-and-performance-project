using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Services.OsobaServices
{
    public class InsertOsobaService : Transaction<Osoba>
    {
        private Osoba _osoba = new();

        public InsertOsobaService(DBContext context) : base(context) { }

        public override Osoba Method()
        {
            _ = dbContext.Set<Osoba>().FromSql($"exec sp_osoba_insert @ime={_osoba.Ime}, @prezime={_osoba.Prezime}, @datum_rodjenja={_osoba.DatumRodjenja}, @jmbg={_osoba.Jmbg}, @broj_telefona={_osoba.BrojTelefona}, @rodno_mesto={_osoba.RodnoMesto};");
            dbContext.SaveChanges();
            if (_osoba != null && _osoba.Prebivaliste.M != 0)
                dbContext.Set<Prebivaliste>().FromSql($"exec sp_prebivaliste_insert @o={_osoba.O}, @m={_osoba.Prebivaliste.M}");
            return _osoba?? new Osoba();
        }

        public Osoba Osoba{ set { _osoba = value; } }
    }
}
