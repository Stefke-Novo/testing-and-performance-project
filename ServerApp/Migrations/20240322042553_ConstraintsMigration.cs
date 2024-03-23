using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerApp.Migrations
{
    /// <inheritdoc />
    public partial class ConstraintsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Check constraints
            migrationBuilder.AddCheckConstraint("CK_mesto_naziv", "mesto", "naziv in ('Суботица','Београд','Нови Сад','Сомбор','Пирот','Призрен')");
            migrationBuilder.AddCheckConstraint("CK_mesto_broj_stanovnika", "mesto", "broj_stanovnika>0 and broj_stanovnika<2000000");


            //Identity constraint
            migrationBuilder.Sql("DBCC CHECKIDENT ('osoba', RESEED, 1000)");

            //Check constraints
            migrationBuilder.AddCheckConstraint("CK_osoba_datum_rodjenja", "osoba", "datum_rodjenja<getdate() and datum_rodjenja>='1950-01-01'");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
