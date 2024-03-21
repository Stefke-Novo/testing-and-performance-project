using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerApp.Migrations
{
    /// <inheritdoc />
    public partial class DeleteOsobaSP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var command = @"create procedure sp_osoba_delete
                            (
                                @o bigint,
                                @prebivaliste bigint
                            )
                            as
                            begin
                            if not exists(select * from osoba where osoba.o = @o)
                                throw 70001, 'ERROR: Osoba with the id doesn''t exist.',1;
                            if @prebivaliste>0 and not exists(select * from mesto where mesto.m = @prebivaliste)
                                throw 70001, 'ERROR: Residence is not registered in system. Please, check support.',1;
                            begin try
                                begin transaction
                                    if @prebivaliste>0
                                        delete from prebivaliste where o=@o and m=@prebivaliste;
                                    delete from osoba where osoba.o = @o;
                                commit transaction
                            end try 
                            begin catch
                                rollback transaction;
                                throw 70001, 'ERROR Residence of the user is registered in the system. Please, delete residence first',1;
                            end catch
                            end";
            migrationBuilder.Sql(command);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var command = @"drop procedure sp_osoba_delete";
            migrationBuilder.Sql(command);
        }
    }
}
