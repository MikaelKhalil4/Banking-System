using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EventLogService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DbCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eventtype",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    type_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("eventtypes_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("transactions_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "eventlog",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    event_type_id = table.Column<int>(type: "integer", nullable: true),
                    event_data = table.Column<string>(type: "jsonb", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    transaction_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("eventlogs_pkey", x => x.id);
                    table.ForeignKey(
                        name: "eventlogs_event_type_id_fkey",
                        column: x => x.event_type_id,
                        principalTable: "eventtype",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "eventlogs_transactions_id_fkey",
                        column: x => x.transaction_id,
                        principalTable: "transaction",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_eventlog_event_type_id",
                table: "eventlog",
                column: "event_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_eventlog_transaction_id",
                table: "eventlog",
                column: "transaction_id");

            migrationBuilder.CreateIndex(
                name: "eventtypes_type_name_key",
                table: "eventtype",
                column: "type_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eventlog");

            migrationBuilder.DropTable(
                name: "eventtype");

            migrationBuilder.DropTable(
                name: "transaction");
        }
    }
}
