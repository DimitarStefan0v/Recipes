using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipesApp.Infrastructure.Migrations
{
    public partial class FixFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloudImages_AspNetUsers_AddedByUserIs",
                table: "CloudImages");

            migrationBuilder.RenameColumn(
                name: "AddedByUserIs",
                table: "CloudImages",
                newName: "AddedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CloudImages_AddedByUserIs",
                table: "CloudImages",
                newName: "IX_CloudImages_AddedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CloudImages_AspNetUsers_AddedByUserId",
                table: "CloudImages",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloudImages_AspNetUsers_AddedByUserId",
                table: "CloudImages");

            migrationBuilder.RenameColumn(
                name: "AddedByUserId",
                table: "CloudImages",
                newName: "AddedByUserIs");

            migrationBuilder.RenameIndex(
                name: "IX_CloudImages_AddedByUserId",
                table: "CloudImages",
                newName: "IX_CloudImages_AddedByUserIs");

            migrationBuilder.AddForeignKey(
                name: "FK_CloudImages_AspNetUsers_AddedByUserIs",
                table: "CloudImages",
                column: "AddedByUserIs",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
