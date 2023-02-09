namespace Recipes.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Recipes.Data.Models;
    using Recipes.Services.Data;
    using Recipes.Web.ViewModels.Recipes;

    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : Controller
    {
        private readonly IRecipesService recipesService;
        private readonly UserManager<ApplicationUser> userManager;

        public FavoritesController(
            IRecipesService recipesService,
            UserManager<ApplicationUser> userManager)
        {
            this.recipesService = recipesService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<bool> Post(PostFavoriteRecipeInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (this.recipesService.IsRecipeInUserFavorites(input.RecipeId, user.Id))
            {
                await this.recipesService.RemoveRecipeFromFavoritesAsync(input.RecipeId, user.Id);
                return false;
            }
            else
            {
                await this.recipesService.AddRecipeToFavoritesAsync(input.RecipeId, user.Id);
                return true;
            }
        }
    }
}
