using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicSearchApp.Migrations
{
    /// <inheritdoc />
    public partial class ChagnedNewsSubscriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_AspNetUsers_PublisherId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_PublisherId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "SubscriberNumber",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "News");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "News",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_News_UserId",
                table: "News",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_AspNetUsers_UserId",
                table: "News",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_AspNetUsers_UserId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_UserId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "News");

            migrationBuilder.AddColumn<int>(
                name: "SubscriberNumber",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_News_PublisherId",
                table: "News",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_AspNetUsers_PublisherId",
                table: "News",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId");
        }
    }
}
