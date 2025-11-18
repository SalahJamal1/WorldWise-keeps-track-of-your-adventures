using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MappyApplication.Migrations
{
    /// <inheritdoc />
    public partial class modifyType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "986ad4d1-799e-4cdd-a789-5a864268be4d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbd87768-1388-40ad-938c-0dc1cd4fc0d9");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "986ad4d1-799e-4cdd-a789-5a864268be4d", null, "user", "USER" },
                    { "fbd87768-1388-40ad-938c-0dc1cd4fc0d9", null, "admin", "ADMIN" }
                });
        }
    }
}
