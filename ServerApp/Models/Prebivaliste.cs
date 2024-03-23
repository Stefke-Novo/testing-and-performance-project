
using Microsoft.EntityFrameworkCore;
using ServerApp.Config;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServerApp.Models
{
    [EntityTypeConfiguration(typeof(PrebivalisteConfiguration))]
    public class Prebivaliste
    {
        public Prebivaliste(long o, long m)
        {
            O = o;
            M = m;
            OsobaInstance= new(){ O = o };
            MestoInstance= new(){ M = m };
        }
        public long O { get; set; } = default;

        public long M { get; set; } = default;

        [NotMapped, JsonIgnore]
        public Osoba OsobaInstance { get; set; } = null!;

        [NotMapped, JsonIgnore]
        public Mesto MestoInstance { get; set; } = null!;
    }
}
