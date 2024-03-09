using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerApp.Migrations
{
    /// <inheritdoc />
    public partial class InsertOsobaSPandMestoData : Migration
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
								@rodno_mesto bigint
							)
							as
							begin
								--null constraint
								if @ime is null throw 70001, 'ERROR: field ime is null.',1;
								if @prezime is null throw 70001, 'ERROR: field prezime is null.',1;
								if @jmbg is null throw 70001, 'ERROR: field jmbg is null.',1;
								if @rodno_mesto is null throw 70001, 'ERROR: field rodno_mesto is null.',1;
								--specific constraints
								if @datum_rodjenja<'1950-01-01' OR @datum_rodjenja>getdate() 
									throw 70001, 'ERROR: Field datum_rodjenja must be between date 1.1.1950 and curent date.',1;
								if not exists(select * from mesto where mesto.m = @rodno_mesto)
										throw 70001, 'ERROR: Typed birthplace doesn''t exist in system.',1;
								begin try
									begin transaction
										insert into osoba (ime, prezime, datum_rodjenja, jmbg, broj_telefona, rodno_mesto)
												output inserted.o, inserted.ime, inserted.prezime, inserted.datum_rodjenja, inserted.jmbg, inserted.broj_telefona, inserted.rodno_mesto
													values (@ime,@prezime,@datum_rodjenja, @jmbg, @broj_telefona, @rodno_mesto);
									commit transaction;
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
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var command = @"drop procedure dbo.sp_osoba_insert_all
							(
								@ime nvarchar(33),
								@prezime nvarchar(33),
								@datum_rodjenja date,
								@jmbg nchar(13),
								@broj_telefona nvarchar(18),
								@rodno_mesto bigint
							)
							as
							begin
								--null constraint
								if @ime is null throw 70001, 'ERROR: field ime is null.',1;
								if @prezime is null throw 70001, 'ERROR: field prezime is null.',1;
								if @jmbg is null throw 70001, 'ERROR: field jmbg is null.',1;
								if @rodno_mesto is null throw 70001, 'ERROR: field rodno_mesto is null.',1;
								--specific constraints
								if @datum_rodjenja<'1950-01-01' OR @datum_rodjenja>getdate() 
									throw 70001, 'ERROR: Field datum_rodjenja must be between date 1.1.1950 and curent date.',1;
								if not exists(select * from mesto where mesto.m = @rodno_mesto)
										throw 70001, 'ERROR: Typed birthplace doesn''t exist in system.',1;
								begin try
									begin transaction
										insert into osoba (ime, prezime, datum_rodjenja, jmbg, broj_telefona, rodno_mesto)
													values (@ime,@prezime,@datum_rodjenja, @jmbg, @broj_telefona, @rodno_mesto);
									commit transaction;
								end try
								begin catch
									rollback transaction
								end catch
							end;";
            migrationBuilder.Sql(command);
			var command1 = @"
			delete from mesto where ptt_broj like '24000';
			delete from mesto where ptt_broj like '11000';
			delete from mesto where ptt_broj like '25000';
			delete from mesto where ptt_broj like '18300';
			delete from mesto where ptt_broj like '20000';
			delete from mesto where ptt_broj like '21000';
			";
			migrationBuilder.Sql(command1);
		}
    }
}
