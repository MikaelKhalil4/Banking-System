using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TransactionService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DBCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true),
                    branch_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("accounts_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "currency",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    currency_code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    currency_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("currency_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transactiontype",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    type_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("transactiontypes_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    account_id = table.Column<int>(type: "integer", nullable: true),
                    amount = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                    transaction_type_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    currency_id = table.Column<int>(type: "integer", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("transactions_pkey", x => x.id);
                    table.ForeignKey(
                        name: "transactions_account_id_fkey",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "transactions_currency_id_fkey",
                        column: x => x.currency_id,
                        principalTable: "currency",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "transactions_transaction_type_id_fkey",
                        column: x => x.transaction_type_id,
                        principalTable: "transactiontype",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "recurrent_transaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    transaction_id = table.Column<int>(type: "integer", nullable: true),
                    bgJob_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("recurrent_transactions_pkey", x => x.id);
                    table.ForeignKey(
                        name: "recurrent_transactions_Transactions_fk",
                        column: x => x.transaction_id,
                        principalTable: "transaction",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "currency_currency_code_key",
                table: "currency",
                column: "currency_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_recurrent_transaction_transaction_id",
                table: "recurrent_transaction",
                column: "transaction_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_account_id",
                table: "transaction",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_currency_id",
                table: "transaction",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_transaction_type_id",
                table: "transaction",
                column: "transaction_type_id");

            migrationBuilder.CreateIndex(
                name: "transactiontypes_type_name_key",
                table: "transactiontype",
                column: "type_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recurrent_transaction");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "currency");

            migrationBuilder.DropTable(
                name: "transactiontype");
        }
    }
}
