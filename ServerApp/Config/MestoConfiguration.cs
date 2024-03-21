using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerApp.Models;

namespace ServerApp.Config
{
    public class MestoConfiguration : IEntityTypeConfiguration<Mesto>
    {
        public void Configure(EntityTypeBuilder<Mesto> builder)
        {
            builder.HasMany<Prebivaliste>().WithOne().HasForeignKey(p=>p.M).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany<Osoba>().WithOne().HasForeignKey(o=>o.RodnoMesto).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
