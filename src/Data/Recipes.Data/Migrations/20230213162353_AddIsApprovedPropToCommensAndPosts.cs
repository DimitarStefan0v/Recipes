#nullable disable

namespace Recipes.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddIsApprovedPropToCommensAndPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ip",
                table: "Messages");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
