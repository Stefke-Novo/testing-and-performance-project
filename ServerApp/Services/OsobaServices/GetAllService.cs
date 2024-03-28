using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;
using ServerApp.Models;

namespace ServerApp.Services.OsobaServices
{
    public class GetAllService : OsobaInfoService<Osoba>
    {
        

        public GetAllService(DBContext dbContext) :base(dbContext) { }
        public List<Osoba> GetAll(int index, int size)
        {
            List<Osoba> result = [.. (from osoba in dbContext.Osobe
            join prebivaliste in dbContext.Prebivalista on osoba.O equals prebivaliste.O into prebivalisteJoin
            from prebivaliste in prebivalisteJoin.DefaultIfEmpty()
            join m1 in dbContext.Mesta on osoba.RodnoMesto equals m1.M into m1Join
            from m1 in m1Join.DefaultIfEmpty()
            join m2 in dbContext.Mesta on prebivaliste.M equals m2.M into m2Join
            from m2 in m2Join.DefaultIfEmpty()
                    select new Osoba()
                    {
                        O = osoba.O,
                        Ime = osoba.Ime,
                        Prezime = osoba.Prezime,
                        DatumRodjenja = osoba.DatumRodjenja,
                        Starost=osoba.Starost,
                        Jmbg=osoba.Jmbg,
                        BrojTelefona=osoba.BrojTelefona,
                        RodnoMesto=osoba.RodnoMesto,
                        Mesto=m1,
                        Prebivaliste=m2??new Mesto()
                    })];
            return PaginatedList<Osoba>.Create(result,index>0?index:1,size>0?size:5);
        }

        public override Osoba Method()
        {
            throw new NotImplementedException();
        }
    }
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public PaginatedList(List<T> items, int count , int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static List<T> Create(List<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count;
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize).ToList();
        }
    }
}
