using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerApp.Models;

namespace ServerApp.Config
{
    public class PrebivalisteConfiguration : IEntityTypeConfiguration<Prebivaliste>
    {
        public void Configure(EntityTypeBuilder<Prebivaliste> builder)
        {
            builder.ToTable("prebivaliste");
            builder.HasKey(p => new { p.O,p.M});
            builder.HasIndex(p => p.O).IsUnique();
            builder.Property(p => p.O).HasColumnName("o").HasColumnType("bigint");
            builder.Property(p => p.M).HasColumnType("bigint").HasColumnName("m");
            builder.HasOne<Osoba>().WithMany().HasForeignKey(p => p.O).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Mesto>().WithMany().HasForeignKey(p => p.M).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
