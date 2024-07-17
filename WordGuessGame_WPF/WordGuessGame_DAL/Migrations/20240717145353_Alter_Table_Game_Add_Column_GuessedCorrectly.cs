using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordGuessGame_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Table_Game_Add_Column_GuessedCorrectly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GuessedCorrectly",
                table: "Game",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuessedCorrectly",
                table: "Game");
        }
    }
}
