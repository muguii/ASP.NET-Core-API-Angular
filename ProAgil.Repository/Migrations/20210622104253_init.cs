using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProAgil.Repository.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EVENTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataEvento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tema = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QtdPessoas = table.Column<int>(type: "int", nullable: false),
                    ImagemURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVENTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PALESTRANTE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiniCurriculo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagemURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PALESTRANTE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LOTE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOTE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LOTE_EVENTO_EventoId",
                        column: x => x.EventoId,
                        principalTable: "EVENTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PALESTRANTE_EVENTO",
                columns: table => new
                {
                    PalestranteId = table.Column<int>(type: "int", nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PALESTRANTE_EVENTO", x => new { x.EventoId, x.PalestranteId });
                    table.ForeignKey(
                        name: "FK_PALESTRANTE_EVENTO_EVENTO_EventoId",
                        column: x => x.EventoId,
                        principalTable: "EVENTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PALESTRANTE_EVENTO_PALESTRANTE_PalestranteId",
                        column: x => x.PalestranteId,
                        principalTable: "PALESTRANTE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REDE_SOCIAL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventoId = table.Column<int>(type: "int", nullable: true),
                    PalestranteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REDE_SOCIAL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_REDE_SOCIAL_EVENTO_EventoId",
                        column: x => x.EventoId,
                        principalTable: "EVENTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_REDE_SOCIAL_PALESTRANTE_PalestranteId",
                        column: x => x.PalestranteId,
                        principalTable: "PALESTRANTE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LOTE_EventoId",
                table: "LOTE",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_PALESTRANTE_EVENTO_PalestranteId",
                table: "PALESTRANTE_EVENTO",
                column: "PalestranteId");

            migrationBuilder.CreateIndex(
                name: "IX_REDE_SOCIAL_EventoId",
                table: "REDE_SOCIAL",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_REDE_SOCIAL_PalestranteId",
                table: "REDE_SOCIAL",
                column: "PalestranteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LOTE");

            migrationBuilder.DropTable(
                name: "PALESTRANTE_EVENTO");

            migrationBuilder.DropTable(
                name: "REDE_SOCIAL");

            migrationBuilder.DropTable(
                name: "EVENTO");

            migrationBuilder.DropTable(
                name: "PALESTRANTE");
        }
    }
}
