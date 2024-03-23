using ServerApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerApp.Config;

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
            modelBuilder.ApplyConfiguration(new OsobaConfiguration());
            modelBuilder.ApplyConfiguration(new MestoConfiguration());
            modelBuilder.ApplyConfiguration(new PrebivalisteConfiguration());
        }
    }


}
