using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Space.MovieSearcher.Infrastructure.Migrations
{
    public partial class UpdateWatchlistMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastOfferDateTime",
                table: "WatchlistMovie",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastOfferDateTime",
                table: "WatchlistMovie");
        }
    }
}
