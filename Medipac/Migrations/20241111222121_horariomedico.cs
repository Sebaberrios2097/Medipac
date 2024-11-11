using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medipac.Migrations
{
    /// <inheritdoc />
    public partial class horariomedico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Res_HorarioMedico",
                columns: table => new
                {
                    Id_Horario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Medico = table.Column<int>(type: "int", nullable: false),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    HoraInicio = table.Column<int>(type: "int", nullable: false),
                    HoraFIn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Res_HorarioMedico", x => x.Id_Horario);
                    table.ForeignKey(
                        name: "FK_Res_HorarioMedico_CLI_Medico",
                        column: x => x.Id_Medico,
                        principalTable: "CLI_Medico",
                        principalColumn: "Id_Medico");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Res_HorarioMedico_Id_Medico",
                table: "Res_HorarioMedico",
                column: "Id_Medico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Res_HorarioMedico");
        }
    }
}
