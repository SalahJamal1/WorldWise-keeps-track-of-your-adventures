using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MappyApplication.Migrations
{
    /// <inheritdoc />
    public partial class modifyType1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1857b26f-fdc3-4ad7-a6d5-bc6311677669");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b415885-e8a8-4b87-80b9-8619f874bff6");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "1857b26f-fdc3-4ad7-a6d5-bc6311677669", null, "admin", "ADMIN" },
                    { "8b415885-e8a8-4b87-80b9-8619f874bff6", null, "user", "USER" }
                });
        }
    }
}
