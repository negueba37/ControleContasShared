using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleContasData.Migrations
{
    /// <inheritdoc />
    public partial class CardDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestPurchaseDate",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Maturity",
                table: "Cards");

            migrationBuilder.AddColumn<int>(
                name: "BestPurchaseDay",
                table: "Cards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaturityDay",
                table: "Cards",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestPurchaseDay",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "MaturityDay",
                table: "Cards");

            migrationBuilder.AddColumn<DateTime>(
                name: "BestPurchaseDate",
                table: "Cards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Maturity",
                table: "Cards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
