using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebParser.Migrations
{
    public partial class Page_PageGroup_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_PageGroups_PageGroupId",
                table: "Pages");

            migrationBuilder.AlterColumn<string>(
                name: "PageGroupId",
                table: "Pages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_PageGroups_PageGroupId",
                table: "Pages",
                column: "PageGroupId",
                principalTable: "PageGroups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_PageGroups_PageGroupId",
                table: "Pages");

            migrationBuilder.AlterColumn<string>(
                name: "PageGroupId",
                table: "Pages",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_PageGroups_PageGroupId",
                table: "Pages",
                column: "PageGroupId",
                principalTable: "PageGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
