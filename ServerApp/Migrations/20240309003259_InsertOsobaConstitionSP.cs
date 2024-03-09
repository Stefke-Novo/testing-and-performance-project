using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerApp.Migrations
{
    /// <inheritdoc />
    public partial class InsertOsobaConstitionSP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var command = @"create procedure sp_osoba_insert_condition
                                (
	                                @rodno_mesto bigint
                                )
                                as
                                begin
	                                if not exists(select * from mesto where mesto.m = @rodno_mesto)
			                                select 'false';
	                                select 'true';
                                end;";
            migrationBuilder.Sql(command);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var command = @"drop procedure sp_osoba_insert_condition
                                (
	                                @rodno_mesto bigint
                                )
                                as
                                begin
	                                if not exists(select * from mesto where mesto.m = @rodno_mesto)
			                                select 'false';
	                                select 'true';
                                end;";
            migrationBuilder.Sql(command);
        }
    }
}
