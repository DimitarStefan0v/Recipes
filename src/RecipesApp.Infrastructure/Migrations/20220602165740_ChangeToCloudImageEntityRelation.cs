using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipesApp.Infrastructure.Migrations
{
    public partial class ChangeToCloudImageEntityRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloudImages_Recipes_RecipeId",
                table: "CloudImages");

            migrationBuilder.DropIndex(
                name: "IX_CloudImages_RecipeId",
                table: "CloudImages");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloudImages_Recipes_RecipeId",
                table: "CloudImages");

            migrationBuilder.DropIndex(
                name: "IX_CloudImages_RecipeId",
                table: "CloudImages");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Recipes");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "CloudImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_CloudImages_RecipeId",
                table: "CloudImages",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CloudImages_Recipes_RecipeId",
                table: "CloudImages",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }
    }
}
