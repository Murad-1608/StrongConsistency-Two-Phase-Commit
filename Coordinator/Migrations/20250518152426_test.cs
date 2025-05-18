using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Coordinator.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Nodes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0861ca34-44e1-443c-99ea-68b575e5200f"), "Order.Api" },
                    { new Guid("257c3c9a-6cc9-45a3-87c7-c07ccccc0809"), "Stock.Api" },
                    { new Guid("7f997d2f-3e28-4fe8-a5c0-cc8e2000338c"), "Payment.Api" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: new Guid("0861ca34-44e1-443c-99ea-68b575e5200f"));

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: new Guid("257c3c9a-6cc9-45a3-87c7-c07ccccc0809"));

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: new Guid("7f997d2f-3e28-4fe8-a5c0-cc8e2000338c"));

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
    }
}
