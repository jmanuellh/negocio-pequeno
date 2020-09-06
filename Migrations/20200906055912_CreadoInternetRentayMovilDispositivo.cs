using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace negocio_pequeño.Migrations
{
    public partial class CreadoInternetRentayMovilDispositivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovilDispositivos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Mac = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovilDispositivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InternetRentas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaFin = table.Column<DateTime>(nullable: false),
                    Dinero = table.Column<int>(nullable: false),
                    MovilDispositivoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternetRentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternetRentas_MovilDispositivos_MovilDispositivoId",
                        column: x => x.MovilDispositivoId,
                        principalTable: "MovilDispositivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternetRentas_MovilDispositivoId",
                table: "InternetRentas",
                column: "MovilDispositivoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternetRentas");

            migrationBuilder.DropTable(
                name: "MovilDispositivos");
        }
    }
}
