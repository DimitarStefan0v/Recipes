using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesApp.Infrastructure.Data
{
    public class RecipeIngredient
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Recipe))]
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        [ForeignKey(nameof(Ingredient))]
        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }

        public string Quantity { get; set; }
    }
}
