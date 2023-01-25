#nullable disable

namespace Recipes.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddEmailPropToMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Messages");
        }
    }
}
