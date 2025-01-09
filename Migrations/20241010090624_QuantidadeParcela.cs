using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleContasData.Migrations
{
    /// <inheritdoc />
    public partial class QuantidadeParcela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstallmentQuantity",
                table: "Accounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallmentQuantity",
                table: "Accounts");
        }
    }
}
