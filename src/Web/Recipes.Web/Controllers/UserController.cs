namespace Recipes.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Recipes.Data.Models;
    using Recipes.Services.Data;
    using Recipes.Web.ViewModels.Recipes;

    [Authorize]
    public class UserController : Controller
    {
        private readonly IUsersService usersService;
        private readonly ICountsService countsService;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(
            IUsersService usersService,
            ICountsService countsService,
            UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.countsService = countsService;
            this.userManager = userManager;
        }

        public IActionResult PersonalRecipes()
        {
            return this.View();
        }

        public async Task<IActionResult> FavoriteRecipes(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            int itemsPerPage = 9;

            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                ItemsCount = this.countsService.GetFavoriteRecipesCount(user.Id),
                Recipes = this.usersService.GetFavorites<RecipeInListViewModel>(id, itemsPerPage, user.Id),
            };

            viewModel.ControllerName = this.ControllerContext.ActionDescriptor.ControllerName;
            viewModel.ActionName = this.ControllerContext.ActionDescriptor.ActionName;

            return this.View(viewModel);
        }
    }
}
