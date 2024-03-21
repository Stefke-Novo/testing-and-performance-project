using ServerApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ServerApp
{
    public class DBContext : DbContext
    {
        public DBContext() : base() { }
        public DBContext(DbContextOptions<DBContext> options) : base(options) {
        }
        public DbSet<Osoba> Osobe { get; set; }
        public DbSet<Mesto> Mesta { get; set; }
        public DbSet<Prebivaliste> Prebivalista { get; set; }

        /*[DbFunction(Name ="sp_osoba_insert")]
        public virtual IQueryable<Osoba> SPOsobaInsert(string ime, string prezime, DateTime datum_rodjenja, string jmbg, string broj_telefona, long rodno_mesto, long prebivaliste)
        {
            return FromExpression(() => SPOsobaInsert(ime, prezime, datum_rodjenja, jmbg, broj_telefona, rodno_mesto, prebivaliste));
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*MethodInfo result = typeof(DBContext).GetMethod(nameof(SPOsobaInsert), new[] { typeof(string), typeof(string), typeof(DateTime), typeof(string), typeof(string), typeof(long), typeof(long) }) ?? throw new Exception("Method info is not initialized.");
            modelBuilder.HasDbFunction(result).HasName("sp_osoba_insert");*/
            /*modelBuilder.Ignore<Mesto>();
            modelBuilder.Entity<Osoba>().OwnsOne<Mesto>(o => o.RodnoMestoInstance);*/
            modelBuilder.Entity<Osoba>().HasKey(o => o.O);
        }
    }


}
