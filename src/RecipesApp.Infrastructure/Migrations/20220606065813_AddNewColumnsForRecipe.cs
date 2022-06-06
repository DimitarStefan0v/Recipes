using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipesApp.Infrastructure.Migrations
{
    public partial class AddNewColumnsForRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "Recipes");
        }
    }
}
