using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda.Api.Migrations
{
    public partial class InsertTelefoneTipos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO TelefoneTipos (Id, Nome) VALUES (1, 'Telefone'), (2, 'Celular')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM TelefoneTipos");
        }
    }
}
