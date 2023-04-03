﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boredtopia.Migrations
{
    /// <inheritdoc />
    public partial class rps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_rpsStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScissorWins = table.Column<int>(type: "int", nullable: false),
                    RockWins = table.Column<int>(type: "int", nullable: false),
                    PaperWins = table.Column<int>(type: "int", nullable: false),
                    Highscore = table.Column<int>(type: "int", nullable: false),
                    TotalGames = table.Column<int>(type: "int", nullable: false),
                    WinPercentDecimal = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__rpsStats", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_rpsStats");
        }
    }
}
