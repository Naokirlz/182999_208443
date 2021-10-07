using Microsoft.EntityFrameworkCore.Migrations;

namespace Incidentes.Datos.Migrations
{
    public partial class prueba_adm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdministradorProyecto",
                columns: table => new
                {
                    AdministradoresId = table.Column<int>(type: "int", nullable: false),
                    proyectosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministradorProyecto", x => new { x.AdministradoresId, x.proyectosId });
                    table.ForeignKey(
                        name: "FK_AdministradorProyecto_Proyectos_proyectosId",
                        column: x => x.proyectosId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdministradorProyecto_Usuarios_AdministradoresId",
                        column: x => x.AdministradoresId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdministradorProyecto_proyectosId",
                table: "AdministradorProyecto",
                column: "proyectosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdministradorProyecto");
        }
    }
}
