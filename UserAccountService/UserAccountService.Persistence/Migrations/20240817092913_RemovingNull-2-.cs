using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAccountService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovingNull2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "branches_location_id_fkey",
                table: "branch");

            migrationBuilder.AlterColumn<int>(
                name: "location_id",
                table: "branch",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "branches_location_id_fkey",
                table: "branch",
                column: "location_id",
                principalTable: "location",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "branches_location_id_fkey",
                table: "branch");

            migrationBuilder.AlterColumn<int>(
                name: "location_id",
                table: "branch",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "branches_location_id_fkey",
                table: "branch",
                column: "location_id",
                principalTable: "location",
                principalColumn: "id");
        }
    }
}
