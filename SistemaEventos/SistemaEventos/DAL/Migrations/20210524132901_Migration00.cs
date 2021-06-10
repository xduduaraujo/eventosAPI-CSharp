using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaEventos.DAL.Migrations
{
    public partial class Migration00 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaEvento",
                columns: table => new
                {
                    IdCategoriaEvento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCategoria = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaEvento", x => x.IdCategoriaEvento);
                });

            migrationBuilder.CreateTable(
                name: "StatusEvento",
                columns: table => new
                {
                    IdEventoStatus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeStatus = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusEvento", x => x.IdEventoStatus);
                });

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    IdEvento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEventoStatus = table.Column<int>(type: "int", nullable: false),
                    IdCategoriaEvento = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    DataHoraInicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataHoraFim = table.Column<DateTime>(type: "datetime", nullable: false),
                    Local = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    LimiteVagas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.IdEvento);
                    table.ForeignKey(
                        name: "FK_Evento_CategoriaEvento",
                        column: x => x.IdCategoriaEvento,
                        principalTable: "CategoriaEvento",
                        principalColumn: "IdCategoriaEvento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_EventoStatus",
                        column: x => x.IdEventoStatus,
                        principalTable: "StatusEvento",
                        principalColumn: "IdEventoStatus",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Participacao",
                columns: table => new
                {
                    IdParticipacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEvento = table.Column<int>(type: "int", nullable: false),
                    LoginParticipante = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    FlagPresente = table.Column<bool>(type: "bit", nullable: false),
                    Nota = table.Column<int>(type: "int", nullable: true),
                    Comentario = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participacao", x => x.IdParticipacao);
                    table.ForeignKey(
                        name: "FK_Participacao_Evento",
                        column: x => x.IdEvento,
                        principalTable: "Evento",
                        principalColumn: "IdEvento",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evento_IdCategoriaEvento",
                table: "Evento",
                column: "IdCategoriaEvento");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_IdEventoStatus",
                table: "Evento",
                column: "IdEventoStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Participacao_IdEvento",
                table: "Participacao",
                column: "IdEvento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participacao");

            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "CategoriaEvento");

            migrationBuilder.DropTable(
                name: "StatusEvento");
        }
    }
}
