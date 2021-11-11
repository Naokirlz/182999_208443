using Microsoft.EntityFrameworkCore.Migrations;

namespace Incidentes.Datos.Migrations
{
    public partial class se_quita_usuarioid_de_incidente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Incidentes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Incidentes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
