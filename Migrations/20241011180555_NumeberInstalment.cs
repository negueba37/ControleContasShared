using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleContasData.Migrations
{
    /// <inheritdoc />
    public partial class NumeberInstalment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumeberInstalment",
                table: "Installments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeberInstalment",
                table: "Installments");
        }
    }
}
