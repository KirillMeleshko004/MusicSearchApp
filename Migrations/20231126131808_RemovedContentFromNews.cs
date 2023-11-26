using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicSearchApp.Migrations
{
    /// <inheritdoc />
    public partial class RemovedContentFromNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "News");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
