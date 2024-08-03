using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordGuessGame_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountOfLetters = table.Column<byte>(type: "tinyint", nullable: false),
                    CorrectWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfTurns = table.Column<byte>(type: "tinyint", nullable: false),
                    AmountOfGuesses = table.Column<byte>(type: "tinyint", nullable: false),
                    GuessedCorrectly = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
