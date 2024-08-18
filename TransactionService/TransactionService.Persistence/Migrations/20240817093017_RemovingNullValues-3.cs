using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovingNullValues3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "recurrent_transactions_Transactions_fk",
                table: "recurrent_transaction");

            migrationBuilder.AlterColumn<int>(
                name: "transaction_id",
                table: "recurrent_transaction",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "bgJob_id",
                table: "recurrent_transaction",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "recurrent_transactions_Transactions_fk",
                table: "recurrent_transaction",
                column: "transaction_id",
                principalTable: "transaction",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "recurrent_transactions_Transactions_fk",
                table: "recurrent_transaction");

            migrationBuilder.AlterColumn<int>(
                name: "transaction_id",
                table: "recurrent_transaction",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "bgJob_id",
                table: "recurrent_transaction",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "recurrent_transactions_Transactions_fk",
                table: "recurrent_transaction",
                column: "transaction_id",
                principalTable: "transaction",
                principalColumn: "id");
        }
    }
}
