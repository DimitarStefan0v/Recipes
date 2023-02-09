﻿namespace Recipes.Web.Controllers
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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;
        private readonly IRecipesService recipesService;

        public FavoritesController(
            UserManager<ApplicationUser> userManager,
            IUsersService usersService,
            IRecipesService recipesService)
        {
            this.userManager = userManager;
            this.usersService = usersService;
            this.recipesService = recipesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<bool> Post(PostFavoriteRecipeInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (this.recipesService.IsRecipeInUserFavorites(input.RecipeId, user.Id))
            {
                await this.usersService.RemoveRecipeFromFavoritesAsync(input.RecipeId, user.Id);
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
