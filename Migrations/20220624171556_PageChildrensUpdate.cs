using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebParser.Migrations
{
    public partial class PageChildrensUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Pages",
                newName: "Updated");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "Pages",
                newName: "LastModified");
        }
    }
}
