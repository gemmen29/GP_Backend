using Microsoft.EntityFrameworkCore.Migrations;

namespace Twitter.Repository.Migrations
{
    public partial class updaterelationtweetreply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reply_ReplyId",
                table: "Reply");

            migrationBuilder.DropIndex(
                name: "IX_Reply_TweetId",
                table: "Reply");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_ReplyId",
                table: "Reply",
                column: "ReplyId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reply_ReplyId",
                table: "Reply");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_ReplyId",
                table: "Reply",
                column: "ReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_TweetId",
                table: "Reply",
                column: "TweetId",
                unique: true);
        }
    }
}
