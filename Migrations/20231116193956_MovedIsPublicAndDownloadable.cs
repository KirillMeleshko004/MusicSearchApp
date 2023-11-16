using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicSearchApp.Migrations
{
    /// <inheritdoc />
    public partial class MovedIsPublicAndDownloadable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Downloadable",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Songs");

            migrationBuilder.AddColumn<bool>(
                name: "Downloadable",
                table: "Albums",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Albums",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Downloadable",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Albums");

            migrationBuilder.AddColumn<bool>(
                name: "Downloadable",
                table: "Songs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Songs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
