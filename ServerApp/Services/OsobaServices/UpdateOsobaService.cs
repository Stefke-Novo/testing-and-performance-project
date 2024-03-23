using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ServerApp.Models;

namespace ServerApp.Services.OsobaServices
{
    public class UpdateOsobaService : OsobaInfoService<Osoba>
    {
        public UpdateOsobaService(DBContext context) : base(context){}

        public override Osoba Method()
        {
            _osoba.Prebivaliste ??= new Mesto();
            Osoba result = dbContext.Osobe.FromSql($"exec procedure sp_update_osoba @o={_osoba.Ime},@ime={_osoba.Ime},@prezime={_osoba.Prezime},@jmbg={_osoba.Jmbg},@datum_rodjenja={_osoba.DatumRodjenja},@broj_telefona={_osoba.BrojTelefona},@rodno_mesto={_osoba.RodnoMesto},@prebivaliste={_osoba.Prebivaliste.M};").AsEnumerable().Single();
            return result;
        }
    }
}
