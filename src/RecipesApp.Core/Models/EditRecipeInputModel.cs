using RecipesApp.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace RecipesApp.Core.Models
{
    public class EditRecipeInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RecipeErrorMessages.RecipeNameRequired)]
        [MinLength(3, ErrorMessage = RecipeErrorMessages.RecipeNameLength)]
        [MaxLength(50, ErrorMessage = RecipeErrorMessages.RecipeNameLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = RecipeErrorMessages.RecipeInstructionsRequired)]
        [MinLength(10, ErrorMessage = RecipeErrorMessages.RecipeInstructionsLength)]
        public string Instructions { get; set; }

        public int PreparationTime { get; set; }

        public int CookingTime { get; set; }

        public int? PortionsCount { get; set; }

        public int CategoryId { get; set; }

        public ICollection<CategoriesViewModel>? Categories { get; set; }

    }
}
