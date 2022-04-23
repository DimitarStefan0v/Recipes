using System.ComponentModel.DataAnnotations;

namespace RecipesApp.Infrastructure.Data
{
    public class RecipeIngredient
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        [Required]
        public Guid IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        [Required]
        [StringLength(50)]
        public string Quantity { get; set; }
    }
}
