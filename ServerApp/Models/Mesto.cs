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
        public long M { get; set; }

        [RegularExpression("^(1[1-9]|2[1-6]|3[1-8])[0-9]{3}$", ErrorMessage = "ERROR: PTT number is incorect.")]
        public string? PttBroj { get; set; } = null;

        [RegularExpression("Нови Сад|Ниш|Пирот|Сомбор|Београд|Суботица")]
        public string Naziv { get; set; } = string.Empty;

        public int BrojStanovnika { get; set; }

        [JsonIgnore]
        public List<Osoba> Osobe { get; set; }

        public Mesto(long rodnoMesto) : this()
        {
            this.M = rodnoMesto;
            this.Osobe = [];
        }

        public Mesto()
        {
            M = default;
            Osobe = [];
        }
    }
}
