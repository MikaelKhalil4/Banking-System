using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "recurrent_transactions_Transactions_fk",
                table: "recurrent_transaction");

            migrationBuilder.RenameColumn(
                name: "transaction_id",
                table: "recurrent_transaction",
                newName: "TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_recurrent_transaction_transaction_id",
                table: "recurrent_transaction",
                newName: "IX_recurrent_transaction_TransactionId");

            migrationBuilder.AlterColumn<long>(
                name: "TransactionId",
                table: "recurrent_transaction",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_recurrent_transaction_transaction_TransactionId",
                table: "recurrent_transaction",
                column: "TransactionId",
                principalTable: "transaction",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recurrent_transaction_transaction_TransactionId",
                table: "recurrent_transaction");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "recurrent_transaction",
                newName: "transaction_id");

            migrationBuilder.RenameIndex(
                name: "IX_recurrent_transaction_TransactionId",
                table: "recurrent_transaction",
                newName: "IX_recurrent_transaction_transaction_id");

            migrationBuilder.AlterColumn<long>(
                name: "transaction_id",
                table: "recurrent_transaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "recurrent_transactions_Transactions_fk",
                table: "recurrent_transaction",
                column: "transaction_id",
                principalTable: "transaction",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
