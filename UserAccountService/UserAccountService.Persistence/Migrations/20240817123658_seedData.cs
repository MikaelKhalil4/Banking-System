using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserAccountService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "location",
                columns: new[] { "id", "location_name" },
                values: new object[,]
                {
                    { 1, "Location 1" },
                    { 2, "Location 2" }
                });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "role_name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Employee" },
                    { 3, "Customer" }
                });

            migrationBuilder.InsertData(
                table: "branch",
                columns: new[] { "id", "location_id", "name" },
                values: new object[,]
                {
                    { 1, 1, "Branch 1" },
                    { 2, 2, "Branch 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "branch",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "branch",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "location",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "location",
                keyColumn: "id",
                keyValue: 2);
        }
    }
}
