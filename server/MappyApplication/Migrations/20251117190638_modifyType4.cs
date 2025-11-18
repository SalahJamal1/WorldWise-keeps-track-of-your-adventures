using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MappyApplication.Migrations
{
    /// <inheritdoc />
    public partial class modifyType4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f54a575-77d1-48fe-9e0f-37fb459e7caf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8b2437d-8bdd-42df-a36a-5cd6591b0cc1");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Workouts",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "211545a6-9532-483c-8d88-3868e96f929f", null, "admin", "ADMIN" },
                    { "b8c971d3-f79d-417d-9456-c94931883121", null, "user", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "211545a6-9532-483c-8d88-3868e96f929f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8c971d3-f79d-417d-9456-c94931883121");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Workouts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f54a575-77d1-48fe-9e0f-37fb459e7caf", null, "user", "USER" },
                    { "f8b2437d-8bdd-42df-a36a-5cd6591b0cc1", null, "admin", "ADMIN" }
                });
        }
    }
}
