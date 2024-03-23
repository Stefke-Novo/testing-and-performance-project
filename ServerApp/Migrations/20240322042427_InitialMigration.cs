using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mesto",
                columns: table => new
                {
                    m = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ptt_broj = table.Column<string>(type: "nchar(5)", nullable: false),
                    naziv = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    broj_stanovnika = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mesto", x => x.m);
                });

            migrationBuilder.CreateTable(
                name: "osoba",
                columns: table => new
                {
                    o = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1000, 1"),
                    ime = table.Column<string>(type: "nvarchar(33)", maxLength: 33, nullable: false),
                    prezime = table.Column<string>(type: "nvarchar(33)", maxLength: 33, nullable: false),
                    datum_rodjenja = table.Column<DateOnly>(type: "date", nullable: false),
                    starost = table.Column<int>(type: "int", nullable: false, computedColumnSql: "datediff(month,[datum_rodjenja],getdate())"),
                    jmbg = table.Column<string>(type: "nchar(13)", nullable: false),
                    broj_telefona = table.Column<string>(type: "nvarchar(18)", nullable: false),
                    rodno_mesto = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_osoba", x => x.o);
                    table.ForeignKey(
                        name: "FK_osoba_mesto_rodno_mesto",
                        column: x => x.rodno_mesto,
                        principalTable: "mesto",
                        principalColumn: "m",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "prebivaliste",
                columns: table => new
                {
                    o = table.Column<long>(type: "bigint", nullable: false),
                    m = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prebivaliste", x => new { x.o, x.m });
                    table.ForeignKey(
                        name: "FK_prebivaliste_mesto_m",
                        column: x => x.m,
                        principalTable: "mesto",
                        principalColumn: "m",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_prebivaliste_osoba_o",
                        column: x => x.o,
                        principalTable: "osoba",
                        principalColumn: "o",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_osoba_jmbg",
                table: "osoba",
                column: "jmbg",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_osoba_rodno_mesto",
                table: "osoba",
                column: "rodno_mesto");

            migrationBuilder.CreateIndex(
                name: "IX_prebivaliste_m",
                table: "prebivaliste",
                column: "m");

            migrationBuilder.CreateIndex(
                name: "IX_prebivaliste_o",
                table: "prebivaliste",
                column: "o",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prebivaliste");

            migrationBuilder.DropTable(
                name: "osoba");

            migrationBuilder.DropTable(
                name: "mesto");
        }
    }
}
