using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAccountService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovingNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "users_branch_id_fkey",
                table: "user");

            migrationBuilder.AlterColumn<int>(
                name: "branch_id",
                table: "user",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "users_branch_id_fkey",
                table: "user",
                column: "branch_id",
                principalTable: "branch",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "users_branch_id_fkey",
                table: "user");

            migrationBuilder.AlterColumn<int>(
                name: "branch_id",
                table: "user",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "users_branch_id_fkey",
                table: "user",
                column: "branch_id",
                principalTable: "branch",
                principalColumn: "id");
        }
    }
}
