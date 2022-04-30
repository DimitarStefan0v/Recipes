using System.ComponentModel.DataAnnotations;

namespace RecipesApp.Core.Models
{
    public class IngredientInputModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Quantity { get; set; }
    }
}
