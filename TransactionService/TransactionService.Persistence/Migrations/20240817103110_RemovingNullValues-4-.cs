using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovingNullValues4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "transactions_transaction_type_id_fkey",
                table: "transaction");

            migrationBuilder.AlterColumn<int>(
                name: "transaction_type_id",
                table: "transaction",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "transactions_transaction_type_id_fkey",
                table: "transaction",
                column: "transaction_type_id",
                principalTable: "transactiontype",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "transactions_transaction_type_id_fkey",
                table: "transaction");

            migrationBuilder.AlterColumn<int>(
                name: "transaction_type_id",
                table: "transaction",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "transactions_transaction_type_id_fkey",
                table: "transaction",
                column: "transaction_type_id",
                principalTable: "transactiontype",
                principalColumn: "id");
        }
    }
}
