using Microsoft.EntityFrameworkCore.Migrations;

namespace Incidentes.Datos.Migrations
{
    public partial class proyecto_id_en_tarea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Proyectos_ProyectoId",
                table: "Tareas");

            migrationBuilder.AlterColumn<int>(
                name: "ProyectoId",
                table: "Tareas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Proyectos_ProyectoId",
                table: "Tareas",
                column: "ProyectoId",
                principalTable: "Proyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Proyectos_ProyectoId",
                table: "Tareas");

            migrationBuilder.AlterColumn<int>(
                name: "ProyectoId",
                table: "Tareas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Proyectos_ProyectoId",
                table: "Tareas",
                column: "ProyectoId",
                principalTable: "Proyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
