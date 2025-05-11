using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Coordinator.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Nodes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("35566328-373c-4b2b-8150-e5914fa1a4f1"), "Stock.Api" },
                    { new Guid("ccb7e7e6-e35f-4127-a749-7100ad2d8d08"), "Order.Api" },
                    { new Guid("d9181895-663a-4f98-995b-b323dc390d28"), "Payment.Api" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: new Guid("35566328-373c-4b2b-8150-e5914fa1a4f1"));

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: new Guid("ccb7e7e6-e35f-4127-a749-7100ad2d8d08"));

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: new Guid("d9181895-663a-4f98-995b-b323dc390d28"));
        }
    }
}
