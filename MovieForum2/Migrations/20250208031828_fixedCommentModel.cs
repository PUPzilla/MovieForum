using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieForum2.Migrations
{
    /// <inheritdoc />
    public partial class fixedCommentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Discussion_discussionId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "discussionId",
                table: "Comment",
                newName: "DiscussionId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_discussionId",
                table: "Comment",
                newName: "IX_Comment_DiscussionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Discussion_DiscussionId",
                table: "Comment",
                column: "DiscussionId",
                principalTable: "Discussion",
                principalColumn: "DiscussionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Discussion_DiscussionId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "DiscussionId",
                table: "Comment",
                newName: "discussionId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_DiscussionId",
                table: "Comment",
                newName: "IX_Comment_discussionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Discussion_discussionId",
                table: "Comment",
                column: "discussionId",
                principalTable: "Discussion",
                principalColumn: "DiscussionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
