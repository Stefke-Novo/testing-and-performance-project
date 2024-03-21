using Microsoft.EntityFrameworkCore;
using ServerApp.Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServerApp.Models
{
    [Table("mesto")]
    [EntityTypeConfiguration(typeof(MestoConfiguration))]
    public class Mesto
    {
        [Column("m"), Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("m")]
        public long M {  get; set; }

        [Column("ptt_broj"), StringLength(5),RegularExpression("^(1[1-9]|2[1-6]|3[1-8])[0-9]{3}$", ErrorMessage ="ERROR: PTT number is incorect.")]
        [JsonPropertyName("ptt_broj")]
        public string? PttBroj { get; set; } = null;

        [Column("naziv"), Required, MaxLength(20),RegularExpression("Нови Сад|Ниш|Пирот|Сомбор|Београд|Суботица")]
        [JsonPropertyName("naziv")]
        public string Naziv { get; set; } = string.Empty;

        [Column("broj_stanovnika"), Required]
        [JsonPropertyName("broj_stanovnika")]
        public int BrojStanovnika { get; set; }

        [JsonIgnore, NotMapped]
        public List<Osoba> osobeRodnogMesta = [];

        [JsonIgnore, NotMapped]
        public List<Prebivaliste> PrebivalistaInstances = [];


    }
}
