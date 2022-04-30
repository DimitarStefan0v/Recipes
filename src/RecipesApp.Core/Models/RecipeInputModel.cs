using System.ComponentModel.DataAnnotations;

namespace RecipesApp.Core.Models
{
    public class RecipeInputModel
    {
        public RecipeInputModel()
        {
            this.Ingredients = new HashSet<IngredientInputModel>();
        }

        [Required]
        [MinLength(3)]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Instructions { get; set; }

        public int? PreparationTime { get; set; }

        public int? CookingTime { get; set; }

        public int? PortionsCount { get; set; }

        public int Category { get; set; }

        public ICollection<IngredientInputModel> Ingredients { get; set; }
    }
}
