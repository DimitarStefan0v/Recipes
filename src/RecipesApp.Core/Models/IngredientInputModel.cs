using System.ComponentModel.DataAnnotations;

namespace RecipesApp.Core.Models
{
    public class IngredientInputModel
    {
        [Required(ErrorMessage = "Името на съставката e задължително")]
        [MinLength(2, ErrorMessage = "Името на съставката трябва е с дължина между 2 и 10 символа")]
        [MaxLength(15)]
        public string IngredientName { get; set; }

        [Required(ErrorMessage = "Количеството на съставката e задължително")]
        [MinLength(1, ErrorMessage = "Количеството на съставката трябва е с дължина между 1 и 10 символа")]
        [MaxLength(15)]
        public string Quantity { get; set; }
    }
}
