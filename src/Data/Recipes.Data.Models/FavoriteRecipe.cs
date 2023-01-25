namespace Recipes.Data.Models
{
    using Recipes.Data.Common.Models;

    public class FavoriteRecipe : BaseDeletableModel<int>
    {
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
