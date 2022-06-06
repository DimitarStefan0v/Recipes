using System.ComponentModel.DataAnnotations;

namespace RecipesApp.Infrastructure.Data
{
    public class Ingredient
    {
        public Ingredient()
        {
            Recipes = new HashSet<RecipeIngredient>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<RecipeIngredient> Recipes { get; set; }
    }
}
