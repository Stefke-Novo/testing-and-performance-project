using Microsoft.EntityFrameworkCore;
using ServerApp;
using ServerApp.Models;
using System;
using System.Linq;
using Xunit;

namespace OsobaServiceTest
{
    public class InsertOsobaServiceTest
    {
        readonly DbContextOptionsBuilder<DBContext> optionsBuilder = new();
        readonly DBContext dbContext = null!;
        private readonly ServerApp.Services.OsobaService osobaService;

        public InsertOsobaServiceTest()
        {
            dbContext = new DBContext(optionsBuilder.UseSqlServer("Data Source=DESKTOP-L8LPP6M;Initial Catalog=testiranje_i_preformanse_seminarski;User ID=root;Password=root;Encrypt=True;Trust Server Certificate=True").Options);
            osobaService = new(dbContext);
        }

        [Fact]
        public void Insert_Should_ReturnInsertedEntityWIthId()
        {
            //Arrange
            Osoba testObject = new(0, "Марко", "Марковић", new DateOnly(2000, 3, 2), "2930493827128", "+381(64)3043023", 1, 1);
            //Act
            Osoba result = osobaService.Insert(testObject);
            //Assert
            Assert.NotEqual(testObject.O, result.O);    
        }
        [Theory]
        [InlineData(0, "Марко", "Марковић", "2000-03-04" , "2930493827128", "+381(64)3043023", 1, 1)]
        [InlineData(0, "МAрко", "Марковић", "2000-03-04", "2930493827128", "+381(64)3043023", 1, 1)]
        [InlineData(0, "Марко", "МAрковић", "2000-03-04", "2930493827128", "+381(64)3043023", 1, 1)]
        [InlineData(0, "Mirko", "Марковић", "2000-03-04", "2930493827128", "+381(64)3043023", 1, 1)]
        [InlineData(0, "Марко", "Mirkovic", "2000-03-04", "2930493827128", "+381(64)3043023", 1, 1)]
        public void Insert_Should_ReturnException_WhenValuesAreFalse(long id,string ime,string prezime, string datumRodjenja,string jmbg, string brojTelefona, long rodnoMesto, long prebivaliste)
        {
            //Arrange
            Osoba testObject = new(id, ime, prezime, DateOnly.Parse(datumRodjenja), jmbg, brojTelefona, rodnoMesto, prebivaliste);
            //Assert
            Assert.ThrowsAny<Exception>(() =>osobaService.Insert(testObject));
        }
        [Fact]
        public void Insert_Should_ReturnEcxpetion_WhenPrebivalisteIdIsInvalid()
        {
            //Assert
            Osoba testObject = new(0, "Марко", "Марковић", new DateOnly(2000, 3, 4), "2930493827128", "+381(64)3043023", 1, 1)
            {
                Prebivaliste = new Mesto() { M = 100 }
            };
            //Assert
            Assert.ThrowsAny<Exception>(() => osobaService.Insert(testObject));
        }
    }
    public class GetAllServiceTest
    {
        readonly DbContextOptionsBuilder<DBContext> optionsBuilder = new();
        readonly DBContext dbContext = null!;
        private readonly ServerApp.Services.OsobaService osobaService;

        public GetAllServiceTest()
        {
            dbContext = new DBContext(optionsBuilder.UseSqlServer("Data Source=DESKTOP-L8LPP6M;Initial Catalog=testiranje_i_preformanse_seminarski;User ID=root;Password=root;Encrypt=True;Trust Server Certificate=True").Options);
            osobaService = new(dbContext);
        }


        [Fact]
        public void GetAll_Should_ReturnOsobaList()
        {
            Assert.NotEmpty(osobaService.GetAll());
        }
    }
    public class UpdateOsobaServiceTest
    {
        DbContextOptionsBuilder<DBContext> optionsBuilder = new();
        readonly DBContext dbContext = null!;
        private readonly ServerApp.Services.OsobaService osobaService;

        public UpdateOsobaServiceTest()
        {
            dbContext = new DBContext(optionsBuilder.UseSqlServer("Data Source=DESKTOP-L8LPP6M;Initial Catalog=testiranje_i_preformanse_seminarski;User ID=root;Password=root;Encrypt=True;Trust Server Certificate=True").Options);
            osobaService = new(dbContext);
        }

        [Theory]
        [InlineData(1010, "Станко", "Марковић", "2000-03-04", "2930493827128", "+381(64)3043023", 1, 1)]
        [InlineData(1010, "Станко", "Маринковић", "2000-03-04", "2930493827128", "+381(64)3043023", 1, 1)]
        [InlineData(1010, "Станко", "Маринковић", "2000-04-04", "2930493827128", "+381(64)3043023", 1, 1)]
        [InlineData(1010, "Станко", "Маринковић", "2000-04-04", "2930493827123", "+381(64)3043023", 1, 1)]
        [InlineData(1010, "Станко", "Маринковић", "2000-04-04", "2930493827123", "+381(64)3043025", 1, 1)]
        [InlineData(1010, "Станко", "Маринковић", "2000-04-04", "2930493827123", "+381(64)3043025", 2, 1)]
        [InlineData(1010, "Станко", "Маринковић", "2000-04-04", "2930493827123", "+381(64)3043025", 2, 2)]
        public void Update_Should_ReturnOsobaWithValueChanged(long id, string ime, string prezime , string datumRodjenja, string jmbg, string brojTelefona, long rodnoMesto, long prebivaliste)
        {
            //Arrange
            Osoba to = new(id, ime, prezime, DateOnly.Parse(datumRodjenja), jmbg, brojTelefona, rodnoMesto, prebivaliste);
            Osoba dbo = dbContext.Osobe.
                Join(dbContext.Prebivalista, o => o.O, p => p.O, (o, p) => new Osoba()
                {
                    O = o.O,
                    Ime = o.Ime,
                    Prezime = o.Prezime,
                    DatumRodjenja = o.DatumRodjenja,
                    Jmbg = o.Jmbg,
                    RodnoMesto = o.RodnoMesto,
                    Prebivaliste = new (p.M)
                }).
                Where(o => o.O == id).Single();
            //Act
            osobaService.Update(to);
            //Assert
            Assert.True(to.Ime.Equals(dbo.Ime)==false || to.Prezime.Equals(dbo.Prezime)==false || to.DatumRodjenja.Equals(dbo.DatumRodjenja)==false||to.Jmbg.Equals(dbo.Jmbg)==false||to.BrojTelefona.Equals(dbo.BrojTelefona)||to.RodnoMesto==dbo.RodnoMesto||to.Prebivaliste.M==dbo.Prebivaliste.M);
        }
    }
}