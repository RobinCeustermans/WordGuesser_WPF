using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordGuessGame_DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueConstraintPlayerName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Game_PlayerName",
                table: "Game");

            migrationBuilder.AlterColumn<string>(
                name: "PlayerName",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PlayerName",
                table: "Game",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Game_PlayerName",
                table: "Game",
                column: "PlayerName",
                unique: true);
        }
    }
}
