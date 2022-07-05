using RecipesApp.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace RecipesApp.Core.Models
{
    public class IngredientInputModel
    {
        [Required(ErrorMessage = RecipeErrorMessages.IngredientNameRequired)]
        [MinLength(2, ErrorMessage = RecipeErrorMessages.IngredientNameLength)]
        [MaxLength(50, ErrorMessage = RecipeErrorMessages.IngredientNameLength)]
        public string IngredientName { get; set; }

        [Required(ErrorMessage = RecipeErrorMessages.QuantityNameRequired)]
        public string Quantity { get; set; }
    }
}
