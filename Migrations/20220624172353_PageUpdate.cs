using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebParser.Migrations
{
    public partial class PageUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PageId",
                table: "Pages",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_PageId",
                table: "Pages",
                column: "PageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Pages_PageId",
                table: "Pages",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Pages_PageId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_PageId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "Pages");
        }
    }
}
