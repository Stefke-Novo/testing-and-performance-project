using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerApp.Migrations
{
    /// <inheritdoc />
    public partial class StoredProceduresMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var command = @"CREATE procedure dbo.sp_osoba_insert
	                        (
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
		                        --null constraint
		                        if @ime is null throw 70001, 'ERROR: field ime is null.',1;
		                        if @prezime is null throw 70001, 'ERROR: field prezime is null.',1;
		                        if @jmbg is null throw 70001, 'ERROR: field jmbg is null.',1;
		                        if @rodno_mesto is null throw 70001, 'ERROR: field rodno_mesto is null.',1;
		                        --specific constraints
                                if exists(select o.jmbg from osoba o where o.jmbg like @jmbg)
                                    throw 70001, 'ERROR: Person with the jmbg is already registered.',1;
		                        if @datum_rodjenja<'1950-01-01' OR @datum_rodjenja>getdate() 
			                        throw 70001, 'ERROR: Field datum_rodjenja must be between date 1.1.1950 and curent date.',1;
		                        if not exists(select * from mesto where mesto.m = @rodno_mesto)
				                        throw 70001, 'ERROR: Typed birthplace doesn''t exist in system.',1;
		                        begin try
			                        declare @result table (o bigint,
									                        ime nvarchar(33),
									                        prezime nvarchar(33),
									                        datum_rodjenja date,
									                        starost int,
									                        jmbg nchar(13),
									                        broj_telefona nvarchar(18),
									                        rodno_mesto bigint);
			                        begin transaction
				                        insert into osoba (ime, prezime, datum_rodjenja, jmbg, broj_telefona, rodno_mesto)
						                        output inserted.o, inserted.ime, inserted.prezime, inserted.datum_rodjenja, inserted.starost, inserted.jmbg, inserted.broj_telefona, inserted.rodno_mesto into @result
							                        values (@ime,@prezime,@datum_rodjenja, @jmbg, @broj_telefona, @rodno_mesto);
				                        if(@prebivaliste>0)
					                        insert into prebivaliste (o,m) values ((select top 1 o from @result),@prebivaliste);
			                        commit transaction;
			                        select * from @result;
		                        end try
		                        begin catch
			                        rollback transaction
		                        end catch
	                        end;";
            migrationBuilder.Sql(command);
            var command1 = @"
                            insert into mesto (ptt_broj, naziv, broj_stanovnika) values ('24000','Суботица',103985);
                            insert into mesto (ptt_broj, naziv, broj_stanovnika) values ('11000','Београд',1374000);
                            insert into mesto (ptt_broj, naziv, broj_stanovnika) values ('25000','Сомбор',41814);
                            insert into mesto (ptt_broj, naziv, broj_stanovnika) values ('18300','Пирот',49894);
                            insert into mesto (ptt_broj, naziv, broj_stanovnika) values ('20000','Призрен',85000);
                            insert into mesto (ptt_broj, naziv, broj_stanovnika) values ('21000','Нови Сад',413823);";
            migrationBuilder.Sql(command1);
            var command2 = @"create procedure sp_osoba_update
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
                                    if @prebivaliste>0 and not exists(select * from mesto where mesto.m = @prebivaliste)
                                            throw 70001, 'ERROR: Residence is not registered. Plseace check support.',1;
                                    begin try
                                        begin transaction
                                                update osoba set ime=@ime, prezime=@prezime, jmbg=@jmbg, @broj_telefona=broj_telefona, datum_rodjenja=datum_rodjenja, rodno_mesto = @rodno_mesto where osoba.o = @o;
                                                
                                                if @prebivaliste>0
                                                    update prebivaliste set m=@prebivaliste where o=@o;
                                        commit transaction
                                        select * from osoba where osoba.o=@o;
                                    end try
                                    begin catch
                                        rollback transaction;
                                    end catch
        
                                end";
            migrationBuilder.Sql(command2);
            var command3 = @"create procedure sp_osoba_delete
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
            migrationBuilder.Sql(command3);
            var command4 = @"create procedure 
                                 sp_prebivaliste_select_by_id
                                 (
                                     @id bigint
                                 )
                                 as
                                 begin
                                     select m.* from prebivaliste p join mesto m on p.m=m.m where p.o=@id;
                                 end;
                                 ";

            migrationBuilder.Sql(command4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
