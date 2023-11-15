using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicSearchApp.Migrations
{
    /// <inheritdoc />
    public partial class RemovedLyrics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lyrics",
                table: "Songs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lyrics",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
