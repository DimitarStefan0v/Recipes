using RecipesApp.Infrastructure.Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesApp.Infrastructure.Data
{
    public class Recipe
    {
        public Recipe()
        {
            Ingredients = new HashSet<RecipeIngredient>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Instructions { get; set; }

        public TimeSpan? PreparationTime { get; set; }

        public TimeSpan? CookingTime { get; set; }

        public  int? PortionsCount { get; set; }

        public Category Category { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public ICollection<RecipeIngredient> Ingredients { get; set; }

        [ForeignKey(nameof(CloudImage))]
        public int ImageId { get; set; }

        public CloudImage Image { get; set; }
    }
}
