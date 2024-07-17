using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _6taMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PacienteProfesional");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PacienteProfesional",
                columns: table => new
                {
                    PacientesId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfesionalesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacienteProfesional", x => new { x.PacientesId, x.ProfesionalesId });
                    table.ForeignKey(
                        name: "FK_PacienteProfesional_Pacientes_PacientesId",
                        column: x => x.PacientesId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacienteProfesional_Profesionales_ProfesionalesId",
                        column: x => x.ProfesionalesId,
                        principalTable: "Profesionales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PacienteProfesional_ProfesionalesId",
                table: "PacienteProfesional",
                column: "ProfesionalesId");
        }
    }
}
