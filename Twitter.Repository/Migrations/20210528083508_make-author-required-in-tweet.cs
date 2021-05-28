using Microsoft.EntityFrameworkCore.Migrations;

namespace Twitter.Repository.Migrations
{
    public partial class makeauthorrequiredintweet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweet_AspNetUsers_AuthorId",
                table: "Tweet");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Tweet",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tweet_AspNetUsers_AuthorId",
                table: "Tweet",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweet_AspNetUsers_AuthorId",
                table: "Tweet");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Tweet",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Tweet_AspNetUsers_AuthorId",
                table: "Tweet",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
