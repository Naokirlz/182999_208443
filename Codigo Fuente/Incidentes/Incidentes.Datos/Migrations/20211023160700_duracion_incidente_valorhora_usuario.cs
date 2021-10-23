using Microsoft.EntityFrameworkCore.Migrations;

namespace Incidentes.Datos.Migrations
{
    public partial class duracion_incidente_valorhora_usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ValorHora",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Duracion",
                table: "Incidentes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorHora",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Duracion",
                table: "Incidentes");
        }
    }
}
