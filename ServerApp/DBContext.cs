using ServerApp.Models;
using Microsoft.EntityFrameworkCore;

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
    }


}
