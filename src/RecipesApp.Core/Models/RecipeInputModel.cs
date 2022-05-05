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

        [Required]
        [MinLength(3)]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Instructions { get; set; }

        public int PreparationTime { get; set; }

        public int CookingTime { get; set; }

        public int? PortionsCount { get; set; }

        public int CategoryId { get; set; }

        public ICollection<CategoriesViewModel> Categories { get; set; }

        public ICollection<IngredientInputModel> Ingredients { get; set; }
    }
}
