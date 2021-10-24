using Microsoft.EntityFrameworkCore.Migrations;

namespace Incidentes.Datos.Migrations
{
    public partial class tareas_y_lista_de_tareas_a_proyecto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Costo = table.Column<int>(type: "int", nullable: false),
                    Duracion = table.Column<int>(type: "int", nullable: false),
                    ProyectoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tareas_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_Id",
                table: "Tareas",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_ProyectoId",
                table: "Tareas",
                column: "ProyectoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tareas");
        }
    }
}
