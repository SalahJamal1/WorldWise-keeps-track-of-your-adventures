using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MappyApplication.Migrations
{
    /// <inheritdoc />
    public partial class modifyLatLng2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45aab2e3-80c9-4815-bbdf-932d8f98fd98");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e33bcb8f-5dc1-45a1-ba79-d5ff284390b5");

            migrationBuilder.AlterColumn<double>(
                name: "Lng",
                table: "Cities",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<double>(
                name: "Lat",
                table: "Cities",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a1bd53d1-028d-4427-a6b8-b9bb5a6f980b", null, "user", "USER" },
                    { "ba3942ea-948e-4142-9edf-e383f8b34323", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1bd53d1-028d-4427-a6b8-b9bb5a6f980b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba3942ea-948e-4142-9edf-e383f8b34323");

            migrationBuilder.AlterColumn<decimal>(
                name: "Lng",
                table: "Cities",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                table: "Cities",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45aab2e3-80c9-4815-bbdf-932d8f98fd98", null, "user", "USER" },
                    { "e33bcb8f-5dc1-45a1-ba79-d5ff284390b5", null, "admin", "ADMIN" }
                });
        }
    }
}
