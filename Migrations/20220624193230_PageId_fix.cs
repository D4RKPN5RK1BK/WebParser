using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebParser.Migrations
{
    public partial class PageId_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "ParentPageId",
                table: "Pages",
                newName: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ParentId",
                table: "Pages",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Pages_ParentId",
                table: "Pages",
                column: "ParentId",
                principalTable: "Pages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Pages_ParentId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_ParentId",
                table: "Pages");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Pages",
                newName: "ParentPageId");

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
    }
}
