
using Microsoft.EntityFrameworkCore;
using ServerApp.Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServerApp.Models
{
    [PrimaryKey(nameof(O),nameof(M))]
    [Table("prebivaliste")]
    [EntityTypeConfiguration(typeof(PrebivalisteConfiguration))]
    [Index(nameof(O),IsUnique = true)]
    public class Prebivaliste
    {
        public Prebivaliste(long o, long m)
        {
            O = o;
            M = m;
            OsobaInstance= new(){ O = o };
            MestoInstance= new(){ M = m };
        }

        [Column("o"),ForeignKey(nameof(Osoba))]
        public long O { get; set; }

        [Column("m"),ForeignKey(nameof(Mesto))]
        public long M { get; set; }

        [NotMapped, JsonIgnore]
        public Osoba OsobaInstance { get; set; }

        [NotMapped, JsonIgnore]
        public Mesto MestoInstance { get; set; }
    }
}
