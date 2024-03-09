
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
    public class Prebivaliste
    {
        public Prebivaliste(long o, long m)
        {
            O = o;
            M = m;
        }

        [Column("o")]
        public long O { get; set; }

        [Column("m")]
        public long M { get; set; }
    }
}
