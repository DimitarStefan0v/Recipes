using RecipesApp.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace RecipesApp.Core.Models
{
    public class RecipeInputModel
    {
        public RecipeInputModel()
        {
            this.Ingredients = new HashSet<IngredientInputModel>();
            this.Categories = new HashSet<CategoriesViewModel>();
        }

        [Required(ErrorMessage = RecipeErrorMessages.RecipeNameRequired)]
        [MinLength(3, ErrorMessage = RecipeErrorMessages.RecipeNameLength)]
        [MaxLength(15, ErrorMessage = RecipeErrorMessages.RecipeNameLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = RecipeErrorMessages.RecipeInstructionsRequired)]
        [MinLength(10, ErrorMessage = RecipeErrorMessages.RecipeInstructionsLength)]
        public string Instructions { get; set; }

        public int PreparationTime { get; set; }

        public int CookingTime { get; set; }

        public int? PortionsCount { get; set; }

        public int CategoryId { get; set; }

        public ICollection<CategoriesViewModel> Categories { get; set; }

        public ICollection<IngredientInputModel> Ingredients { get; set; }
    }
}
