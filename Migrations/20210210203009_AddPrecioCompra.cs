using Microsoft.EntityFrameworkCore.Migrations;

namespace negocio_pequeño.Migrations
{
    public partial class AddPrecioCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecioCompra",
                table: "Product",
                type: "decimal(5, 2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioCompra",
                table: "Product");
        }
    }
}
