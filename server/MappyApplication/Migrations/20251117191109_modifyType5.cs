using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MappyApplication.Migrations
{
    /// <inheritdoc />
    public partial class modifyType5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "211545a6-9532-483c-8d88-3868e96f929f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8c971d3-f79d-417d-9456-c94931883121");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1be3de1e-e2bc-41ca-83c2-6fd3ab91edb1", null, "user", "USER" },
                    { "2f0408f0-2990-442b-97b9-7fa2836d1ae8", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1be3de1e-e2bc-41ca-83c2-6fd3ab91edb1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f0408f0-2990-442b-97b9-7fa2836d1ae8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "211545a6-9532-483c-8d88-3868e96f929f", null, "admin", "ADMIN" },
                    { "b8c971d3-f79d-417d-9456-c94931883121", null, "user", "USER" }
                });
        }
    }
}
