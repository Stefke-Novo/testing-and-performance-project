using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerApp.Models;

namespace ServerApp.Config
{
    public class PrebivalisteConfiguration : IEntityTypeConfiguration<Prebivaliste>
    {
        public void Configure(EntityTypeBuilder<Prebivaliste> builder)
        {
            builder.HasOne<Osoba>().WithMany().HasForeignKey(p => p.O).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Mesto>().WithMany().HasForeignKey(p => p.M).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
