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
        public DBContext(DbContextOptions<DBContext> options) : base(options) {}
        public virtual DbSet<Osoba> Osobe { get; set; }
        public virtual DbSet<Mesto> Mesta { get; set; }
        public virtual DbSet<Prebivaliste> Prebivalista { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OsobaConfiguration());
            modelBuilder.ApplyConfiguration(new MestoConfiguration());
            modelBuilder.ApplyConfiguration(new PrebivalisteConfiguration());
        }
    }


}
