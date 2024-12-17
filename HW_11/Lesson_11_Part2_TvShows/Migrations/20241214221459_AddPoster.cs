using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lesson_11_Part2_TvShows.Migrations
{
    /// <inheritdoc />
    public partial class AddPoster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "TvShow",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poster",
                table: "TvShow");
        }
    }
}
