using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using ServerApp.Models;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using ServerApp.DTO;
using System.Runtime.CompilerServices;

namespace ServerApp.Services.OsobaServices
{
    public class InsertOsobaService : OsobaInfoService
    {

        public InsertOsobaService(DBContext context) : base(context) { }

        public Osoba Method()
        {
            if (_osoba == null) throw new Exception("Object _osoba not initialized.");

            _osoba.Prebivaliste ??= new Mesto();
            /*using (var connection = new SqlConnection(dbContext.Database.GetConnectionString()))
            {
                using var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "[dbo].[sp_osoba_insert]";
                command.Parameters.AddWithValue("@ime", _osoba.Ime);
                command.Parameters.AddWithValue("@prezime", _osoba.Prezime);
                command.Parameters.AddWithValue("@datum_rodjenja", _osoba.DatumRodjenja);
                command.Parameters.AddWithValue("@jmbg", _osoba.Jmbg);
                command.Parameters.AddWithValue("@broj_telefona", _osoba.BrojTelefona);
                command.Parameters.AddWithValue("@rodno_mesto", _osoba.RodnoMesto);
                command.Parameters.AddWithValue("@prebivaliste", _osoba.Prebivaliste.M);
                dbContext.Database.CloseConnection();
                connection.Open();
                _osoba.O = command.ExecuteNonQuery();
                connection.Close();
                dbContext.Database.OpenConnection();
                if (_osoba.O == 0) throw new Exception("Error: object osoba is not saved.");
            }*/
            /*using (var connection = new SqlConnection(dbContext.Database.GetConnectionString()))
            {
                string insert = $"insert into osoba (ime, prezime, datum_rodjenja, jmbg, broj_telefona, rodno_mesto) values ('{_osoba.Ime}','{_osoba.Prezime}','{_osoba.DatumRodjenja.ToString("yyyy-MM-dd")}','{_osoba.Jmbg}','{_osoba.BrojTelefona}',{_osoba.RodnoMesto});";

                using var command = new SqlCommand(insert);
                command.Connection = connection;
                connection.Open();
                _osoba.O = command.ExecuteNonQuery();
                connection.Close();
            }*/
            /*using (var connection = new SqlConnection(dbContext.Database.GetConnectionString()))
            {
                dbContext.Database.BeginTransaction();
                string connectionString = connection.ConnectionString;
                Console.WriteLine(connectionString);
                SqlCommand query = new($"insert into osoba (ime, prezime, datum_rodjenja, jmbg, broj_telefona, rodno_mesto) values ('{_osoba.Ime}','{_osoba.Prezime}', convert(date,'{_osoba.DatumRodjenja:yyyy-MM-dd}'),'{_osoba.Jmbg}','{_osoba.BrojTelefona}',{_osoba.RodnoMesto});", connection);
                connection.Open();
                _osoba.O=query.ExecuteNonQuery();
                connection.Close();
                //dbContext.Database.ExecuteSql(query);
                dbContext.Database.CommitTransaction();
            }*/

            /*dbContext.Database.ExecuteSqlInterpolated($"EXEC sp_osoba_insert '{_osoba.Ime}', '{_osoba.Prezime}','{_osoba.DatumRodjenja}','{_osoba.Jmbg}','{_osoba.BrojTelefona}',{_osoba.RodnoMesto},{_osoba.Prebivaliste.M}");*/

            /*Osoba result =  dbContext.SPOsobaInsert(_osoba.Ime, _osoba.Prezime, _osoba.DatumRodjenja, _osoba.Jmbg, _osoba.BrojTelefona, _osoba.RodnoMesto, _osoba.Prebivaliste.M).Single();*/

            //var result = dbContext.Database.ExecuteSqlRaw("EXEC sp_osoba_insert {0}, {1},{2},{3},{4},{5},{6}",_osoba.Ime,_osoba.Prezime, _osoba.DatumRodjenja, _osoba.Jmbg,_osoba.BrojTelefona,_osoba.RodnoMesto,_osoba.Prebivaliste.M);
            FormattableString query = FormattableStringFactory.Create($"EXEC sp_osoba_insert '{_osoba.Ime}','{_osoba.Prezime}','{_osoba.DatumRodjenja:yyyy-MM-dd}','{_osoba.Jmbg}','{_osoba.BrojTelefona}',{_osoba.RodnoMesto},{_osoba.Prebivaliste.M}");
            var list = dbContext.Database.SqlQuery<OsobaDTO>(query).ToList<OsobaDTO>();
            list.ForEach(l => Console.WriteLine(l.O+" "+l.Ime+" "+l.Prezime));
            //Console.WriteLine(result);
            return _osoba;
        }
    }

    
}
