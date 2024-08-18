using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TransactionService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Seeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "currency",
                columns: new[] { "id", "currency_code", "currency_name" },
                values: new object[,]
                {
                    { 1, "USD", "US Dollar" },
                    { 2, "EUR", "Euro" },
                    { 3, "LBP", "Lebanese Pound" }
                });

            migrationBuilder.InsertData(
                table: "transactiontype",
                columns: new[] { "id", "type_name" },
                values: new object[,]
                {
                    { 1, "Deposit" },
                    { 2, "Withdrawal" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "currency",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "currency",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "currency",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "transactiontype",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "transactiontype",
                keyColumn: "id",
                keyValue: 2);
        }
    }
}
