using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServerApp.Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerApp.Models
{
    [Table("osoba")]
    [EntityTypeConfiguration(typeof(OsobaConfiguration))]
    [Index(nameof(O),IsUnique =true)]
    [Index(nameof(Jmbg), IsUnique = true)]
    public class Osoba
    {
        [Column("o"), Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long O { get; set; } = default;
        [Column("ime"), 
        MaxLength(33),
        RegularExpression(@"^[АБВГДЂЕЖЗИЈКЛМНОПРСТЋУФХЦЧЏШ][абвгдђежзијклмнњопрстћуфхцчџш]+$",
            ErrorMessage = "ERROR: Field ime must be typed in Serbian Cyrilic script. First letter must be uppercase while all other must be lowercase."), 
        Required]
        public string Ime { get; set; } = string.Empty;

        [Column("prezime"), 
            MaxLength(33), 
            RegularExpression(@"^[АБВГДЂЕЖЗИЈКЛМНОПРСТЋУФХЦЧЏШ][абвгдђежзијклмнњопрстћуфхцчџш]+$",
                ErrorMessage = "ERROR: Field prezime must be typed in Serbian Cyrilic script. First letter must be uppercase while all other must be lowercase."),
            Required]
        public string Prezime { get; set; } = string.Empty;

        [Column("datum_rodjenja")]
        public DateOnly DatumRodjenja { get; set; }

        [Column("starost")]
        public int Starost {  get; set; }

        [Column("jmbg"), 
        StringLength(13), 
        RegularExpression("^[0-9]{13}$",
            ErrorMessage = "ERROR: Field jmbg does not have 13 digits which values are from 0 to 9")]
        public string Jmbg { get; set; } = string.Empty;

        [Column("broj_telefona"), 
         MaxLength(18), 
         RegularExpression("^\\+381\\([1-9]{2}\\)[0-9]{5,10}$|^\\(0[1-9]{2}\\)[0-9]{5,10}$", 
            ErrorMessage = "ERROR: Field broj_telefona must contain format like (011)0123456789 or +381(63)12345")]
        public string BrojTelefona { get; set; } = string.Empty;

        [Column("rodno_mesto")]
        public long RodnoMesto { get; set; }
        [NotMapped]
        public Mesto Prebivaliste { get; set; } = new Mesto();
    }
}
