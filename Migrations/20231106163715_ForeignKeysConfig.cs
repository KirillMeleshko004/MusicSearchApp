using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicSearchApp.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeysConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Users_UserId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Users_UserId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Users_UserId",
                table: "Favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Users_UserId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_PublishRequests_Users_UserId",
                table: "PublishRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Genres_GenreName",
                table: "Songs");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Users_UserId",
                table: "Songs");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_UserId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_UserId1",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_UserId1",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Songs_UserId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_PublishRequests_UserId",
                table: "PublishRequests");

            migrationBuilder.DropIndex(
                name: "IX_News_UserId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_Albums_UserId",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Actions_UserId",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "SongId",
                table: "PublishRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PublishRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Actions");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "PublishRequests",
                newName: "ArtistId");

            migrationBuilder.RenameColumn(
                name: "SongId",
                table: "News",
                newName: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ArtistId",
                table: "Subscriptions",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_PublishRequests_AlbumId",
                table: "PublishRequests",
                column: "AlbumId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PublishRequests_ArtistId",
                table: "PublishRequests",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_News_AlbumId",
                table: "News",
                column: "AlbumId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_News_PublisherId",
                table: "News",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_ArtistId",
                table: "Albums",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_ActorId",
                table: "Actions",
                column: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Users_ActorId",
                table: "Actions",
                column: "ActorId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Users_ArtistId",
                table: "Albums",
                column: "ArtistId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserID",
                table: "Comments",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Users_UserId",
                table: "Favourites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Albums_AlbumId",
                table: "News",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "AlbumId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Users_PublisherId",
                table: "News",
                column: "PublisherId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PublishRequests_Albums_AlbumId",
                table: "PublishRequests",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "AlbumId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublishRequests_Users_ArtistId",
                table: "PublishRequests",
                column: "ArtistId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Genres_GenreName",
                table: "Songs",
                column: "GenreName",
                principalTable: "Genres",
                principalColumn: "Name",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Users_ArtistId",
                table: "Songs",
                column: "ArtistId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_ArtistId",
                table: "Subscriptions",
                column: "ArtistId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_SubscriberId",
                table: "Subscriptions",
                column: "SubscriberId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Users_ActorId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Users_ArtistId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Users_UserId",
                table: "Favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Albums_AlbumId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Users_PublisherId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_PublishRequests_Albums_AlbumId",
                table: "PublishRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PublishRequests_Users_ArtistId",
                table: "PublishRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Genres_GenreName",
                table: "Songs");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Users_ArtistId",
                table: "Songs");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_ArtistId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_SubscriberId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_ArtistId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_PublishRequests_AlbumId",
                table: "PublishRequests");

            migrationBuilder.DropIndex(
                name: "IX_PublishRequests_ArtistId",
                table: "PublishRequests");

            migrationBuilder.DropIndex(
                name: "IX_News_AlbumId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_PublisherId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_Albums_ArtistId",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Actions_ActorId",
                table: "Actions");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "PublishRequests",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "AlbumId",
                table: "News",
                newName: "SongId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Songs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SongId",
                table: "PublishRequests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PublishRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "News",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Albums",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Actions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserId1",
                table: "Subscriptions",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_UserId",
                table: "Songs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PublishRequests_UserId",
                table: "PublishRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_News_UserId",
                table: "News",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_UserId",
                table: "Albums",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_UserId",
                table: "Actions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Users_UserId",
                table: "Actions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Users_UserId",
                table: "Albums",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserID",
                table: "Comments",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Users_UserId",
                table: "Favourites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Users_UserId",
                table: "News",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PublishRequests_Users_UserId",
                table: "PublishRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Genres_GenreName",
                table: "Songs",
                column: "GenreName",
                principalTable: "Genres",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Users_UserId",
                table: "Songs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_UserId",
                table: "Subscriptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_UserId1",
                table: "Subscriptions",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
