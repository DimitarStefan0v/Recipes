using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipesApp.Infrastructure.Migrations
{
    public partial class FixRecipeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaregoryId",
                table: "Recipes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaregoryId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
