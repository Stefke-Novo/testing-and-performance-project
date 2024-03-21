using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOsobaSP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var command = @"create procedure sp_osoba_update
                            (
                                @o bigint,
                                @ime nvarchar(33),
                                @prezime nvarchar(33),
                                @datum_rodjenja date,
                                @jmbg nchar(13),
                                @broj_telefona nvarchar(18),
                                @rodno_mesto bigint,
                                @prebivaliste bigint
                            )
                            as
                            begin
                                --value constraints
                                if not exists(select * from mesto where mesto.m=@rodno_mesto)
                                        throw 70001,'ERROR: Birthplace for the user is not registered. Plseace contact supprot.',1;
                                begin try
                                    begin transaction
                                            update osoba set ime=@ime, prezime=@prezime, jmbg=@jmbg, @broj_telefona=broj_telefona, datum_rodjenja=datum_rodjenja, rodno_mesto = @rodno_mesto where osoba.o = @o;
                                            if @prebivaliste>0 and not exists(select * from mesto where mesto.m = @prebivaliste)
                                                throw 70001, 'ERROR: Residence is not registered. Plseace check support.',1;
                                            if @prebivaliste>0
                                                update prebivaliste set m=@prebivaliste where o=@o;
                                    commit transaction
                                end try
                                begin catch
                                    rollback transaction;
                                end catch
                                
                            end";
            migrationBuilder.Sql(command);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var command = @"drop procedure sp_osoba_update";
            migrationBuilder.Sql(command);
        }
    }
}
