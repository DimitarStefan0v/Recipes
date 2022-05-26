using RecipesApp.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace RecipesApp.Core.Models
{
    public class IngredientInputModel
    {
        [Required(ErrorMessage = RecipeErrorMessages.IngredientNameRequired)]
        [MinLength(2, ErrorMessage = RecipeErrorMessages.IngredientNameLength)]
        [MaxLength(10, ErrorMessage = RecipeErrorMessages.IngredientNameLength)]
        public string IngredientName { get; set; }

        [Required(ErrorMessage = RecipeErrorMessages.IngredientQuantityRequired)]
        [MinLength(1, ErrorMessage = RecipeErrorMessages.IngredientQuantityLength)]
        [MaxLength(15, ErrorMessage = RecipeErrorMessages.IngredientQuantityLength)]
        public string Quantity { get; set; }
    }
}
