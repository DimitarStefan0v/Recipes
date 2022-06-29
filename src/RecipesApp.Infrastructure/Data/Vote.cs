using RecipesApp.Infrastructure.Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesApp.Infrastructure.Data
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Recipe))]
        public int? RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }

        public ApplicationUser User { get; set; }

        public byte Value { get; set; }
    }
}
