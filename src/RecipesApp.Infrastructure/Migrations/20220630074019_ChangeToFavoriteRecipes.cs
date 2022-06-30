using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipesApp.Infrastructure.Migrations
{
    public partial class ChangeToFavoriteRecipes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipeId_AspNetUsers_AddedByUserId",
                table: "FavoriteRecipeId");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteRecipeId",
                table: "FavoriteRecipeId");

            migrationBuilder.RenameTable(
                name: "FavoriteRecipeId",
                newName: "FavoriteRecipeIds");

            migrationBuilder.RenameColumn(
                name: "AddedByUserId",
                table: "FavoriteRecipeIds",
                newName: "LikedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRecipeId_AddedByUserId",
                table: "FavoriteRecipeIds",
                newName: "IX_FavoriteRecipeIds_LikedByUserId");

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "FavoriteRecipeIds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteRecipeIds",
                table: "FavoriteRecipeIds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipeIds_AspNetUsers_LikedByUserId",
                table: "FavoriteRecipeIds",
                column: "LikedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipeIds_AspNetUsers_LikedByUserId",
                table: "FavoriteRecipeIds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteRecipeIds",
                table: "FavoriteRecipeIds");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "FavoriteRecipeIds");

            migrationBuilder.RenameTable(
                name: "FavoriteRecipeIds",
                newName: "FavoriteRecipeId");

            migrationBuilder.RenameColumn(
                name: "LikedByUserId",
                table: "FavoriteRecipeId",
                newName: "AddedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRecipeIds_LikedByUserId",
                table: "FavoriteRecipeId",
                newName: "IX_FavoriteRecipeId_AddedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteRecipeId",
                table: "FavoriteRecipeId",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipeId_AspNetUsers_AddedByUserId",
                table: "FavoriteRecipeId",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
