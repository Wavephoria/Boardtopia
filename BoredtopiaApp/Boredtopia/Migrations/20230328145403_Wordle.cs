using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boredtopia.Migrations
{
    /// <inheritdoc />
    public partial class Wordle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_wordleStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordlePlays = table.Column<int>(type: "int", nullable: false),
                    WordleAverage = table.Column<double>(type: "float", nullable: false),
                    WordleBest = table.Column<int>(type: "int", nullable: false),
                    WordleTotal = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__wordleStats", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_wordleStats");
        }
    }
}
