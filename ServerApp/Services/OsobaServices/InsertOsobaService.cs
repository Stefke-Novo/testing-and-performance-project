using Microsoft.EntityFrameworkCore;
using ServerApp.Models;
using System.Collections;
using System.Runtime.CompilerServices;

namespace ServerApp.Services.OsobaServices
{
    public class InsertOsobaService : OsobaInfoService<Osoba>
    {

        public InsertOsobaService(DBContext context) : base(context) { }

        public override Osoba Method()
        {
            if (_osoba == null) throw new Exception("Object _osoba not initialized.");

            _osoba.Prebivaliste ??= new Mesto();
            FormattableString query = $"EXEC sp_osoba_insert {_osoba.Ime},{_osoba.Prezime}, {_osoba.DatumRodjenja.ToString("yyyy-MM-dd")} ,{_osoba.Jmbg},{_osoba.BrojTelefona},{_osoba.Mesto.M},{_osoba.Prebivaliste.M}";
            Osoba result = dbContext.Osobe.FromSql(query).AsEnumerable().Single();

            if (_osoba.Prebivaliste.M > 0)
            {
                result.Prebivaliste = dbContext.Mesta.
                FromSql($"EXEC sp_prebivaliste_select_by_id {result.O};").
                AsEnumerable().Single();
            }

            result.Mesto = dbContext.Mesta.FromSql($"EXEC sp_mesto_select_by_id {_osoba.RodnoMesto}").AsEnumerable().Single();

            return result;
        }
    }


}
