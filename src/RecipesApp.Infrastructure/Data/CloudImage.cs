using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesApp.Infrastructure.Data
{
    public class CloudImage
    {
        [Key]
        public int Id { get; set; }

        public string PicturePublicId { get; set; }

        public string PictureUrl { get; set; }

        [ForeignKey(nameof(Recipe))]
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }
    }
}
