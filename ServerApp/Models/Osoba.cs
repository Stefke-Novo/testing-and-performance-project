using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServerApp.Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServerApp.Models
{
    [EntityTypeConfiguration(typeof(OsobaConfiguration))]
    public class Osoba
    {
        public Osoba() { }

        public Osoba(long o, string ime, string prezime, DateOnly datumRodjenja, string jmbg, string brojTelefona, long rodnoMesto, long prebivaliste) : this()
        {
            this.O = o;
            this.Ime = ime;
            this.Prezime = prezime;
            this.DatumRodjenja = datumRodjenja;
            this.Jmbg = jmbg;
            this.BrojTelefona = brojTelefona;
            this.RodnoMesto = rodnoMesto;
            this.Prebivaliste = new(prebivaliste) { Osobe = [this] };
            this.Mesto = new(rodnoMesto) { Osobe = [this] };
        }
        public long O { get; set; } = default;

        [RegularExpression(@"^[АБВГДЂЕЖЗИЈКЛЉМНЊОПРСТЋУФХЦЧЏШ][абвгдђежзијклљмнњопрстћуфхцчџш]+$",
            ErrorMessage = "ERROR: Field ime must be typed in Serbian Cyrilic script. First letter must be uppercase while all other must be lowercase.")]
        public string Ime { get; set; } = string.Empty;

        [RegularExpression(@"^[АБВГДЂЕЖЗИЈКЛЉМНЊОПРСТЋУФХЦЧЏШ][абвгдђежзијклљмнњопрстћуфхцчџш]+$",
            ErrorMessage = "ERROR: Field prezime must be typed in Serbian Cyrilic script. First letter must be uppercase while all other must be lowercase.")]
        public string Prezime { get; set; } = string.Empty;
        public DateOnly DatumRodjenja { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int Starost { get; set; }

        [RegularExpression("^[0-9]{13}$",
            ErrorMessage = "ERROR: Field jmbg does not have 13 digits which values are from 0 to 9")]
        public string Jmbg { get; set; } = string.Empty;

        [RegularExpression("^\\+381\\([1-9]{2}\\)[0-9]{5,10}$|^\\(0[1-9]{2}\\)[0-9]{5,10}$",
            ErrorMessage = "ERROR: Field broj_telefona must contain format like (011)0123456789 or +381(63)12345")]
        public string BrojTelefona { get; set; } = string.Empty;

        public long RodnoMesto { get; set; }

        [NotMapped, JsonPropertyName("prebivaliste")]
        public Mesto Prebivaliste { get; set; } = new();

        [JsonPropertyName("rodno_mesto")]
        public Mesto Mesto { get; set; } = new();


        public override string? ToString()
        {
            return "Osoba: " + this.Ime + ", " + this.Prezime + ", " + this.DatumRodjenja.ToString("yyy-MM-dd") + ", " + this.Jmbg + ", " + this.BrojTelefona + ", " + this.Starost + ", " + this.RodnoMesto;
        }
    }
}
