using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerApp.Models;

namespace ServerApp.Config
{
    public class OsobaConfiguration : IEntityTypeConfiguration<Osoba>
    {
        public void Configure(EntityTypeBuilder<Osoba> builder)
        {

            builder.ToTable("osoba");
            //Id column
            builder.Property<long>(p => p.O)
                .HasColumnName("o").
                HasColumnType("bigint").
                ValueGeneratedOnAdd().
                HasJsonPropertyName("o").
                IsRequired();
            // primary key constraint
            builder.HasKey(o => o.O);
            //Ime column
            builder.Property<string>(p => p.Ime).
                HasColumnName("ime").
                HasColumnType("nvarchar(33)").
                HasMaxLength(33).
                IsRequired().
                HasJsonPropertyName("ime");
            //Prezime column
            builder.Property<string>(o => o.Prezime).
                HasColumnName("prezime").
                HasColumnType("nvarchar(33)").
                HasMaxLength(33).
                IsRequired().
                HasJsonPropertyName("prezime");
            //DatumRodjenja column
            builder.Property<DateOnly>(o => o.DatumRodjenja).
                HasColumnName("datum_rodjenja").
                HasColumnType("date").
                HasJsonPropertyName("datum_rodjenja");
            //Starost column
            builder.Property(p => p.Starost).
                HasComputedColumnSql("datediff(month,[datum_rodjenja],getdate())").
                HasColumnName("starost").
                HasJsonPropertyName("starost");
            //Jmbg column
            builder.Property<string>(o => o.Jmbg).
                HasJsonPropertyName("jmbg").
                HasColumnName("jmbg").
                HasColumnType("nchar(13)");
            //BrojTelefona column
            builder.Property<string>(p => p.BrojTelefona).
                HasColumnName("broj_telefona").
                HasColumnType("nvarchar(18)").
                HasJsonPropertyName("broj_telefona");
            //RodnoMesto column
            builder.Property<long>(o => o.RodnoMesto).
                HasColumnName("rodno_mesto").
                HasColumnType("bigint").
                HasJsonPropertyName("rodno_mesto_id");
            
            builder.HasIndex(o => o.Jmbg).IsUnique();

            builder.HasOne(o=>o.Mesto).WithMany(m=>m.Osobe).HasForeignKey(o=>o.RodnoMesto).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
