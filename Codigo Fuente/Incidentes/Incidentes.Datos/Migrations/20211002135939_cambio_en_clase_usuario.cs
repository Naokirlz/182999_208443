using Microsoft.EntityFrameworkCore.Migrations;

namespace Incidentes.Datos.Migrations
{
    public partial class cambio_en_clase_usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidentes_Proyectos_ProyectoId",
                table: "Incidentes");

            migrationBuilder.DropTable(
                name: "DesarrolladorProyecto");

            migrationBuilder.DropTable(
                name: "ProyectoTester");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "NombreProyecto",
                table: "Incidentes");

            migrationBuilder.AddColumn<int>(
                name: "RolUsuario",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProyectoId",
                table: "Incidentes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProyectoUsuario",
                columns: table => new
                {
                    AsignadosId = table.Column<int>(type: "int", nullable: false),
                    proyectosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectoUsuario", x => new { x.AsignadosId, x.proyectosId });
                    table.ForeignKey(
                        name: "FK_ProyectoUsuario_Proyectos_proyectosId",
                        column: x => x.proyectosId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyectoUsuario_Usuarios_AsignadosId",
                        column: x => x.AsignadosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoUsuario_proyectosId",
                table: "ProyectoUsuario",
                column: "proyectosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidentes_Proyectos_ProyectoId",
                table: "Incidentes",
                column: "ProyectoId",
                principalTable: "Proyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidentes_Proyectos_ProyectoId",
                table: "Incidentes");

            migrationBuilder.DropTable(
                name: "ProyectoUsuario");

            migrationBuilder.DropColumn(
                name: "RolUsuario",
                table: "Usuarios");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ProyectoId",
                table: "Incidentes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "NombreProyecto",
                table: "Incidentes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DesarrolladorProyecto",
                columns: table => new
                {
                    DesarrolladoresId = table.Column<int>(type: "int", nullable: false),
                    proyectosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesarrolladorProyecto", x => new { x.DesarrolladoresId, x.proyectosId });
                    table.ForeignKey(
                        name: "FK_DesarrolladorProyecto_Proyectos_proyectosId",
                        column: x => x.proyectosId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesarrolladorProyecto_Usuarios_DesarrolladoresId",
                        column: x => x.DesarrolladoresId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProyectoTester",
                columns: table => new
                {
                    TestersId = table.Column<int>(type: "int", nullable: false),
                    proyectosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectoTester", x => new { x.TestersId, x.proyectosId });
                    table.ForeignKey(
                        name: "FK_ProyectoTester_Proyectos_proyectosId",
                        column: x => x.proyectosId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyectoTester_Usuarios_TestersId",
                        column: x => x.TestersId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesarrolladorProyecto_proyectosId",
                table: "DesarrolladorProyecto",
                column: "proyectosId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoTester_proyectosId",
                table: "ProyectoTester",
                column: "proyectosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidentes_Proyectos_ProyectoId",
                table: "Incidentes",
                column: "ProyectoId",
                principalTable: "Proyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
