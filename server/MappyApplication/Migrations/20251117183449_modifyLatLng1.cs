using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MappyApplication.Migrations
{
    /// <inheritdoc />
    public partial class modifyLatLng1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "086c5ae5-7bfe-4659-afd4-72d004cb4d48");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30f1e4bb-b846-44da-84a5-6cb83c0aaba4");

            migrationBuilder.AlterColumn<decimal>(
                name: "Lng",
                table: "Cities",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                table: "Cities",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45aab2e3-80c9-4815-bbdf-932d8f98fd98", null, "user", "USER" },
                    { "e33bcb8f-5dc1-45a1-ba79-d5ff284390b5", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45aab2e3-80c9-4815-bbdf-932d8f98fd98");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e33bcb8f-5dc1-45a1-ba79-d5ff284390b5");

            migrationBuilder.AlterColumn<float>(
                name: "Lng",
                table: "Cities",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<float>(
                name: "Lat",
                table: "Cities",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "086c5ae5-7bfe-4659-afd4-72d004cb4d48", null, "admin", "ADMIN" },
                    { "30f1e4bb-b846-44da-84a5-6cb83c0aaba4", null, "user", "USER" }
                });
        }
    }
}
