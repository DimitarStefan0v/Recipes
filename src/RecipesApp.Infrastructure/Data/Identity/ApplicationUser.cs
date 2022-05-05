using Microsoft.AspNetCore.Identity;

namespace RecipesApp.Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Recipe> Recipes { get; set; } = new HashSet<Recipe>();
    }
}
