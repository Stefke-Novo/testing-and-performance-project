using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerApp.Models;

namespace ServerApp.Config
{
    public class OsobaConfiguration : IEntityTypeConfiguration<Osoba>
    {
        public void Configure(EntityTypeBuilder<Osoba> builder)
        {
            builder.ToTable("osoba").HasKey(o=>o.O);
            builder.Property(p => p.Starost).HasComputedColumnSql("datediff(month,[datum_rodjenja,getdate()])");
            builder.HasOne<Mesto>().WithMany().HasForeignKey(o=>o.RodnoMesto).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany<Prebivaliste>().WithOne().HasForeignKey(p => p.O).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
