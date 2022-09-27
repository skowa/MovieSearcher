using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Space.MovieSearcher.Presentation.Api.Migrations
{
    public partial class CreateMoviesDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Watchlist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watchlist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WatchlistMovie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WatchlistId = table.Column<int>(type: "int", nullable: false),
                    IsMovieWatched = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchlistMovie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchlistMovie_Watchlist_WatchlistId",
                        column: x => x.WatchlistId,
                        principalTable: "Watchlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistMovie_WatchlistId",
                table: "WatchlistMovie",
                column: "WatchlistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchlistMovie");

            migrationBuilder.DropTable(
                name: "Watchlist");
        }
    }
}
