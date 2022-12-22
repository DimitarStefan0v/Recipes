namespace Recipes.Data.Models
{
    using Recipes.Data.Common.Models;

    public class RecipeIngredient : BaseDeletableModel<int>
    {
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }

        public string Quantity { get; set; }
    }
}
