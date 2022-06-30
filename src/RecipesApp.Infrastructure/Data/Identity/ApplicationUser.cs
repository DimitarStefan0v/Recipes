using Microsoft.AspNetCore.Identity;

namespace RecipesApp.Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Recipe> Recipes { get; set; } = new HashSet<Recipe>();

        public ICollection<Vote> Votes { get; set; } = new HashSet<Vote>();

        public ICollection<CloudImage> Images { get; set; } = new HashSet<CloudImage>();

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public ICollection<FavoriteRecipeId> FavoriteRecipeIds { get; set; } = new HashSet<FavoriteRecipeId>();
    }
}
