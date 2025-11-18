using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MappyApplication.Migrations
{
    /// <inheritdoc />
    public partial class modifyLatLngs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9303ebe0-4390-4e4f-a8c0-ccdfbf9521b6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f963ab53-d346-4ff1-88a8-fa4a8070cebd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "086c5ae5-7bfe-4659-afd4-72d004cb4d48", null, "admin", "ADMIN" },
                    { "30f1e4bb-b846-44da-84a5-6cb83c0aaba4", null, "user", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "086c5ae5-7bfe-4659-afd4-72d004cb4d48");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30f1e4bb-b846-44da-84a5-6cb83c0aaba4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9303ebe0-4390-4e4f-a8c0-ccdfbf9521b6", null, "user", "USER" },
                    { "f963ab53-d346-4ff1-88a8-fa4a8070cebd", null, "admin", "ADMIN" }
                });
        }
    }
}
