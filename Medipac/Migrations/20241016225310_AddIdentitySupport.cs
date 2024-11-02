using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medipac.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentitySupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "COM_Estados_Usuario",
                columns: table => new
                {
                    Id_Estado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false, comment: "Columna que representa el borrado lógico del registro.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COM_Estados_Usuario", x => x.Id_Estado);
                });

            migrationBuilder.CreateTable(
                name: "RES_Especialidades",
                columns: table => new
                {
                    Id_Especialidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false, comment: "Columna que representa el borrado lógico del registro.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RES_Especialidades", x => x.Id_Especialidad);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Estado = table.Column<int>(type: "int", nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    Is_Admin = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COM_Usuario_COM_Estados_Usuario",
                        column: x => x.Id_Estado,
                        principalTable: "COM_Estados_Usuario",
                        principalColumn: "Id_Estado");
                });

            migrationBuilder.CreateTable(
                name: "ADM_Noticias",
                columns: table => new
                {
                    Id_Noticia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Subtitulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha_Publicacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    Url_Imagen = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADM_Noticias", x => x.Id_Noticia);
                    table.ForeignKey(
                        name: "FK_ADM_Noticias_COM_Usuario",
                        column: x => x.Id_Usuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CLI_Medico",
                columns: table => new
                {
                    Id_Medico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Rut = table.Column<int>(type: "int", nullable: false),
                    Dv = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ap_Paterno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ap_Materno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Fono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false, comment: "Columna que representa el borrado lógico del registro.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLI_Medico", x => x.Id_Medico);
                    table.ForeignKey(
                        name: "FK_CLI_Medico_COM_Usuario",
                        column: x => x.Id_Usuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CLI_Pacientes",
                columns: table => new
                {
                    Id_Paciente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ap_Paterno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ap_Materno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Rut = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Fono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false, comment: "Columna que representa el borrado lógico del registro.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLI_Pacientes", x => x.Id_Paciente);
                    table.ForeignKey(
                        name: "FK_CLI_Pacientes_COM_Usuario",
                        column: x => x.Id_Usuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LOG_Usuario",
                columns: table => new
                {
                    Id_Log_Usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    Exitoso = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOG_Usuario", x => x.Id_Log_Usuario);
                    table.ForeignKey(
                        name: "FK_LOG_Usuario_COM_Usuario",
                        column: x => x.Id_Usuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ADM_Carrusel_Noticias",
                columns: table => new
                {
                    Id_Carrusel_Noticias = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Noticia = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Subtitulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Url_Imagen = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADM_Carrusel_Noticias", x => x.Id_Carrusel_Noticias);
                    table.ForeignKey(
                        name: "FK_ADM_Carrusel_Noticias_ADM_Noticias",
                        column: x => x.Id_Noticia,
                        principalTable: "ADM_Noticias",
                        principalColumn: "Id_Noticia");
                });

            migrationBuilder.CreateTable(
                name: "RES_Agenda",
                columns: table => new
                {
                    Id_Agenda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Medico = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    Hora_Inicio = table.Column<int>(type: "int", nullable: false),
                    Hora_FIn = table.Column<int>(type: "int", nullable: false),
                    Disponible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RES_Agenda", x => x.Id_Agenda);
                    table.ForeignKey(
                        name: "FK_RES_Agenda_CLI_Medico",
                        column: x => x.Id_Medico,
                        principalTable: "CLI_Medico",
                        principalColumn: "Id_Medico");
                });

            migrationBuilder.CreateTable(
                name: "RES_Medico_Especialidad",
                columns: table => new
                {
                    Id_Medico_Especialidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Medico = table.Column<int>(type: "int", nullable: false),
                    Id_Especialidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RES_Medico_Especialidad", x => x.Id_Medico_Especialidad);
                    table.ForeignKey(
                        name: "FK_RES_Medico_Especialidad_CLI_Medico",
                        column: x => x.Id_Medico,
                        principalTable: "CLI_Medico",
                        principalColumn: "Id_Medico");
                    table.ForeignKey(
                        name: "FK_RES_Medico_Especialidad_RES_Especialidades",
                        column: x => x.Id_Especialidad,
                        principalTable: "RES_Especialidades",
                        principalColumn: "Id_Especialidad");
                });

            migrationBuilder.CreateTable(
                name: "CLI_Historial_Paciente",
                columns: table => new
                {
                    Id_Historial_Paciente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Paciente = table.Column<int>(type: "int", nullable: false),
                    Id_Medico = table.Column<int>(type: "int", nullable: false),
                    Historial = table.Column<string>(type: "text", nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    Fecha_Historial = table.Column<DateTime>(type: "datetime", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false, comment: "Columna que representa el borrado lógico del registro.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLI_Historial_Paciente", x => x.Id_Historial_Paciente);
                    table.ForeignKey(
                        name: "FK_CLI_Historial_Paciente_CLI_Medico",
                        column: x => x.Id_Medico,
                        principalTable: "CLI_Medico",
                        principalColumn: "Id_Medico");
                    table.ForeignKey(
                        name: "FK_CLI_Historial_Paciente_CLI_Pacientes",
                        column: x => x.Id_Paciente,
                        principalTable: "CLI_Pacientes",
                        principalColumn: "Id_Paciente");
                });

            migrationBuilder.CreateTable(
                name: "RES_Reserva",
                columns: table => new
                {
                    Id_Reserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Paciente = table.Column<int>(type: "int", nullable: false),
                    Id_Medico = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false, comment: "Columna que representa el borrado lógico del registro."),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RES_Reserva", x => x.Id_Reserva);
                    table.ForeignKey(
                        name: "FK_RES_Reserva_CLI_Medico",
                        column: x => x.Id_Medico,
                        principalTable: "CLI_Medico",
                        principalColumn: "Id_Medico");
                    table.ForeignKey(
                        name: "FK_RES_Reserva_CLI_Pacientes",
                        column: x => x.Id_Paciente,
                        principalTable: "CLI_Pacientes",
                        principalColumn: "Id_Paciente");
                });

            migrationBuilder.CreateTable(
                name: "CLI_Receta_Paciente",
                columns: table => new
                {
                    Id_Receta_Paciente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Historial_Paciente = table.Column<int>(type: "int", nullable: false),
                    Receta = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false, comment: "Columna que representa el borrado lógico del registro.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLI_Receta_Paciente", x => x.Id_Receta_Paciente);
                    table.ForeignKey(
                        name: "FK_CLI_Receta_Paciente_CLI_Historial_Paciente",
                        column: x => x.Id_Historial_Paciente,
                        principalTable: "CLI_Historial_Paciente",
                        principalColumn: "Id_Historial_Paciente");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ADM_Carrusel_Noticias_Id_Noticia",
                table: "ADM_Carrusel_Noticias",
                column: "Id_Noticia");

            migrationBuilder.CreateIndex(
                name: "IX_ADM_Noticias_Id_Usuario",
                table: "ADM_Noticias",
                column: "Id_Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Id_Estado",
                table: "AspNetUsers",
                column: "Id_Estado");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CLI_Historial_Paciente_Id_Medico",
                table: "CLI_Historial_Paciente",
                column: "Id_Medico");

            migrationBuilder.CreateIndex(
                name: "IX_CLI_Historial_Paciente_Id_Paciente",
                table: "CLI_Historial_Paciente",
                column: "Id_Paciente");

            migrationBuilder.CreateIndex(
                name: "IX_CLI_Medico_Id_Usuario",
                table: "CLI_Medico",
                column: "Id_Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_CLI_Pacientes_Id_Usuario",
                table: "CLI_Pacientes",
                column: "Id_Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_CLI_Receta_Paciente_Id_Historial_Paciente",
                table: "CLI_Receta_Paciente",
                column: "Id_Historial_Paciente");

            migrationBuilder.CreateIndex(
                name: "IX_LOG_Usuario_Id_Usuario",
                table: "LOG_Usuario",
                column: "Id_Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_RES_Agenda_Id_Medico",
                table: "RES_Agenda",
                column: "Id_Medico");

            migrationBuilder.CreateIndex(
                name: "IX_RES_Medico_Especialidad_Id_Especialidad",
                table: "RES_Medico_Especialidad",
                column: "Id_Especialidad");

            migrationBuilder.CreateIndex(
                name: "IX_RES_Medico_Especialidad_Id_Medico",
                table: "RES_Medico_Especialidad",
                column: "Id_Medico");

            migrationBuilder.CreateIndex(
                name: "IX_RES_Reserva_Id_Medico",
                table: "RES_Reserva",
                column: "Id_Medico");

            migrationBuilder.CreateIndex(
                name: "IX_RES_Reserva_Id_Paciente",
                table: "RES_Reserva",
                column: "Id_Paciente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADM_Carrusel_Noticias");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CLI_Receta_Paciente");

            migrationBuilder.DropTable(
                name: "LOG_Usuario");

            migrationBuilder.DropTable(
                name: "RES_Agenda");

            migrationBuilder.DropTable(
                name: "RES_Medico_Especialidad");

            migrationBuilder.DropTable(
                name: "RES_Reserva");

            migrationBuilder.DropTable(
                name: "ADM_Noticias");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CLI_Historial_Paciente");

            migrationBuilder.DropTable(
                name: "RES_Especialidades");

            migrationBuilder.DropTable(
                name: "CLI_Medico");

            migrationBuilder.DropTable(
                name: "CLI_Pacientes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "COM_Estados_Usuario");
        }
    }
}
