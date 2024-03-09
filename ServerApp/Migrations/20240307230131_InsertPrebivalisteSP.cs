using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerApp.Migrations
{
    /// <inheritdoc />
    public partial class InsertPrebivalisteSP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var command = @"create procedure sp_prebivaliste_insert 
                            (
                                @o bigint,
                                @m bigint
                            )
                            as
                            begin
                            if @o = 0 throw 70001, 'ERROR: Id for object osoba is 0. Please, contact support.',1;
                            if @m = 0 throw 70001, 'ERROR: Id for object mesto is not proviced (value is 0). Please, check if your residence is selected correctly. If the message continues, please constact support.',1;
                            begin try
                                begin transaction
                                    insert into prebivaliste (o,m) values (@o,@m);
                                commit transaction
                            end try
                            begin catch
                                rollback transaction
                            end catch
                            end;";
            migrationBuilder.Sql(command);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var command = @"drop procedure sp_prebivaliste_insert 
                            (
                                @o bigint,
                                @m bigint
                            )
                            as
                            begin
                            if @o = 0 throw 70001, 'ERROR: Id for object osoba is 0. Please, contact support.',1;
                            if @m = 0 throw 70001, 'ERROR: Id for object mesto is not proviced (value is 0). Please, check if your residence is selected correctly. If the message continues, please constact support.',1;
                            begin try
                                begin transaction
                                    insert into prebivaliste (o,m) values (@o,@m);
                                commit transaction
                            end try
                            begin catch
                                rollback transaction
                            end catch
                            end;";
            migrationBuilder.Sql(command);
        }
    }
}
