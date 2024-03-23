using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerApp.Models;

namespace ServerApp.Config
{
    public class MestoConfiguration : IEntityTypeConfiguration<Mesto>
    {
        public void Configure(EntityTypeBuilder<Mesto> builder)
        {
            builder.Property<long>(m => m.M).
                HasColumnName("m").
                HasColumnType("bigint").
                ValueGeneratedOnAdd().
                HasJsonPropertyName("m");
            builder.HasKey(m => m.M);
            builder.Property(m => m.PttBroj).
                HasColumnName("ptt_broj").
                HasColumnType("nchar(5)").
                IsRequired().
                HasJsonPropertyName("ptt_broj");
            builder.Property(m => m.Naziv).
                HasColumnType("nvarchar(20)").
                HasColumnName("naziv").
                HasJsonPropertyName("naziv").
                IsRequired();
            builder.Property(m => m.BrojStanovnika).
                HasColumnName("broj_stanovnika").
                HasColumnType("int").
                HasJsonPropertyName("broj_stanovnika").
                IsRequired();
            builder.HasMany(m => m.Osobe).WithOne(o => o.Mesto).HasForeignKey(m => m.RodnoMesto);
        }
    }
}
