using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MappyApplication.Migrations
{
    /// <inheritdoc />
    public partial class modifyLatLng4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2dd9fd53-087f-45e5-853f-ed35168c8e1a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3b82a07-dfca-47f3-9ef0-410c84d77d88");

            migrationBuilder.AlterColumn<double>(
                name: "Lng",
                table: "Cities",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,12)");

            migrationBuilder.AlterColumn<double>(
                name: "Lat",
                table: "Cities",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,12)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "986ad4d1-799e-4cdd-a789-5a864268be4d", null, "user", "USER" },
                    { "fbd87768-1388-40ad-938c-0dc1cd4fc0d9", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "986ad4d1-799e-4cdd-a789-5a864268be4d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbd87768-1388-40ad-938c-0dc1cd4fc0d9");

            migrationBuilder.AlterColumn<decimal>(
                name: "Lng",
                table: "Cities",
                type: "decimal(18,12)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                table: "Cities",
                type: "decimal(18,12)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2dd9fd53-087f-45e5-853f-ed35168c8e1a", null, "user", "USER" },
                    { "a3b82a07-dfca-47f3-9ef0-410c84d77d88", null, "admin", "ADMIN" }
                });
        }
    }
}
