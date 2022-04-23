using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesApp.Infrastructure.Data
{
    public class Recipe
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Instructions { get; set; }

        [Required]
        public Guid CaregoryId { get; set; }

        [ForeignKey(nameof(CaregoryId))]
        public virtual Category Category { get; set; }

        public virtual ICollection<RecipeIngredient> Ingredients { get; set; } = new HashSet<RecipeIngredient>();
    }
}
