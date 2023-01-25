#nullable disable

namespace Recipes.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddViewsCountToRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewsCount",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewsCount",
                table: "Recipes");
        }
    }
}
