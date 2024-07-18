using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _10Migracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApellidoPaciente",
                table: "Turnos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApellidoProfesional",
                table: "Turnos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DNIPaciente",
                table: "Turnos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MatriculaProfesional",
                table: "Turnos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NombrePaciente",
                table: "Turnos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NombreProfesional",
                table: "Turnos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApellidoPaciente",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "ApellidoProfesional",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "DNIPaciente",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "MatriculaProfesional",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "NombrePaciente",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "NombreProfesional",
                table: "Turnos");
        }
    }
}
