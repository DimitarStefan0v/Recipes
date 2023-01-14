#nullable disable

namespace Recipes.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddOneToOneRelatishionWithCategoryAndCloudImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloudImages_Recipes_RecipeId",
                table: "CloudImages");

            migrationBuilder.DropIndex(
                name: "IX_CloudImages_RecipeId",
                table: "CloudImages");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Recipes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "CloudImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "CloudImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CloudImages_CategoryId",
                table: "CloudImages",
                column: "CategoryId",
                unique: true,
                filter: "[CategoryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CloudImages_RecipeId",
                table: "CloudImages",
                column: "RecipeId",
                unique: true,
                filter: "[RecipeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CloudImages_Categories_CategoryId",
                table: "CloudImages",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CloudImages_Recipes_RecipeId",
                table: "CloudImages",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloudImages_Categories_CategoryId",
                table: "CloudImages");

            migrationBuilder.DropForeignKey(
                name: "FK_CloudImages_Recipes_RecipeId",
                table: "CloudImages");

            migrationBuilder.DropIndex(
                name: "IX_CloudImages_CategoryId",
                table: "CloudImages");

            migrationBuilder.DropIndex(
                name: "IX_CloudImages_RecipeId",
                table: "CloudImages");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "CloudImages");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "CloudImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CloudImages_RecipeId",
                table: "CloudImages",
                column: "RecipeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CloudImages_Recipes_RecipeId",
                table: "CloudImages",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
