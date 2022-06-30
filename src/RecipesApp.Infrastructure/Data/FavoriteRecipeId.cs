using RecipesApp.Infrastructure.Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesApp.Infrastructure.Data
{
    public class FavoriteRecipeId
    {
        [Key]
        public int Id { get; set; }

        public int RecipeId { get; set; }

        [ForeignKey(nameof(LikedByUser))]
        public string LikedByUserId { get; set; }

        public ApplicationUser LikedByUser { get; set; }
    }
}
