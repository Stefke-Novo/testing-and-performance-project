using ServerApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ServerApp.DTO
{
    [Keyless]
    public class OsobaDTO
    {
        [Column("o"), Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("o")]
        public long O { get; set; } = default;
        [Column("ime"),
        MaxLength(33),
        RegularExpression(@"^[АБВГДЂЕЖЗИЈКЛЉМНЊОПРСТЋУФХЦЧЏШ][абвгдђежзијклљмнњопрстћуфхцчџш]+$",
            ErrorMessage = "ERROR: Field ime must be typed in Serbian Cyrilic script. First letter must be uppercase while all other must be lowercase."),
        Required,
        JsonPropertyName("ime")]
        public string Ime { get; set; } = string.Empty;

        [Column("prezime"),
            MaxLength(33),
            RegularExpression(@"^[АБВГДЂЕЖЗИЈКЛЉМНЊОПРСТЋУФХЦЧЏШ][абвгдђежзијклљмнњопрстћуфхцчџш]+$",
                ErrorMessage = "ERROR: Field prezime must be typed in Serbian Cyrilic script. First letter must be uppercase while all other must be lowercase."),
            Required,
            JsonPropertyName("prezime")]
        public string Prezime { get; set; } = string.Empty;

        [Column("datum_rodjenja"), JsonPropertyName("datum_rodjenja")]
        public DateTime DatumRodjenja { get; set; } = DateTime.Now;

        [Column("starost"), JsonPropertyName("starost")]
        public int Starost { get; set; }

        [Column("jmbg"),
        StringLength(13),
        RegularExpression("^[0-9]{13}$",
            ErrorMessage = "ERROR: Field jmbg does not have 13 digits which values are from 0 to 9"),
        JsonPropertyName("jmbg")]
        public string Jmbg { get; set; } = string.Empty;

        [Column("broj_telefona"),
         MaxLength(18),
         RegularExpression("^\\+381\\([1-9]{2}\\)[0-9]{5,10}$|^\\(0[1-9]{2}\\)[0-9]{5,10}$",
            ErrorMessage = "ERROR: Field broj_telefona must contain format like (011)0123456789 or +381(63)12345"),
        JsonPropertyName("broj_telefona")]
        public string BrojTelefona { get; set; } = string.Empty;

        [Column("rodno_mesto"), ForeignKey(nameof(Mesto)), JsonPropertyName("rodno_mesto")]
        public long RodnoMesto { get; set; }
        [NotMapped, JsonPropertyName("prebivaliste")]
        public Mesto? Prebivaliste { get; set; } = new Mesto();

        [NotMapped, JsonIgnore]
        public Mesto? RodnoMestoInstance { get; set; }

        [NotMapped, JsonIgnore]
        public List<Prebivaliste> PrebivalsitaInstances { get; set; } = [];
    }
}
