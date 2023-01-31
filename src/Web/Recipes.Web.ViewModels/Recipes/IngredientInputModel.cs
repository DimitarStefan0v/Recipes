namespace Recipes.Web.ViewModels.Recipes
{
    using System.ComponentModel.DataAnnotations;

    using global::Recipes.Common.Constants;

    public class IngredientInputModel
    {
        [MinLength(2, ErrorMessage = RecipeErrorMessages.IngredientNameLength)]
        [MaxLength(50, ErrorMessage = RecipeErrorMessages.IngredientNameLength)]
        public string IngredientName { get; set; }

        public string Quantity { get; set; }
    }
}
