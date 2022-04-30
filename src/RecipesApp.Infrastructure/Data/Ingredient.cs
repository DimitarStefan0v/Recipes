using System.ComponentModel.DataAnnotations;

namespace RecipesApp.Infrastructure.Data
{
    public class Ingredient
    {
        public Ingredient()
        {
            this.Recipes = new HashSet<RecipeIngredient>();
        }
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<RecipeIngredient> Recipes { get; set; }
    }
}
