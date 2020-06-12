using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class updatedAppUserandImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uploads",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_AuthorId",
                table: "Images",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_AuthorId",
                table: "Images",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_AuthorId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_AuthorId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "Uploads",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
