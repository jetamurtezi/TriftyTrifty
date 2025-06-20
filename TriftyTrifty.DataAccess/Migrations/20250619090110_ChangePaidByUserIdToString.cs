using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TriftyTrifty.Migrations
{
    /// <inheritdoc />
    public partial class ChangePaidByUserIdToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_AspNetUsers_PaidByUserId1",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_PaidByUserId1",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "PaidByUserId1",
                table: "Expenses");

            migrationBuilder.AlterColumn<string>(
                name: "PaidByUserId",
                table: "Expenses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PaidByUserId",
                table: "Expenses",
                column: "PaidByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_AspNetUsers_PaidByUserId",
                table: "Expenses",
                column: "PaidByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_AspNetUsers_PaidByUserId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_PaidByUserId",
                table: "Expenses");

            migrationBuilder.AlterColumn<int>(
                name: "PaidByUserId",
                table: "Expenses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "PaidByUserId1",
                table: "Expenses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PaidByUserId1",
                table: "Expenses",
                column: "PaidByUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_AspNetUsers_PaidByUserId1",
                table: "Expenses",
                column: "PaidByUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
