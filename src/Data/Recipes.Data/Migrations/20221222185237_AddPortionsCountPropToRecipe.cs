#nullable disable

namespace Recipes.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddPortionsCountPropToRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PortionsCount",
                table: "Recipes",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PortionsCount",
                table: "Recipes");
        }
    }
}
