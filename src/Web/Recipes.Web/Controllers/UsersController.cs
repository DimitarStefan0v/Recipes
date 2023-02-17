namespace Recipes.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Recipes.Data.Models;
    using Recipes.Services.Data.Contracts;
    using Recipes.Web.ViewModels.Recipes;

    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IRecipesService recipesService;
        private readonly ICountsService countsService;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(
            IRecipesService recipesService,
            ICountsService countsService,
            UserManager<ApplicationUser> userManager)
        {
            this.recipesService = recipesService;
            this.countsService = countsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> FavoriteRecipes(int page = 1)
        {
            if (page <= 0)
            {
                return this.RedirectToAction("Error", "Home");
            }

            int itemsPerPage = 9;

            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
                ItemsCount = this.countsService.GetFavoriteRecipesCount(user.Id),
                Recipes = this.recipesService.GetFavorites<RecipeInListViewModel>(page, itemsPerPage, user.Id),
                ControllerName = this.ControllerContext.ActionDescriptor.ControllerName,
                ActionName = this.ControllerContext.ActionDescriptor.ActionName,
            };

            if (page > viewModel.PagesCount && viewModel.PagesCount > 0)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> PersonalRecipes(string sortOrder = "descending", int page = 1)
        {
            if (page <= 0)
            {
                return this.RedirectToAction("Error", "Home");
            }

            int itemsPerPage = 9;

            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
                ItemsCount = this.countsService.GetPersonalRecipesCount(user.Id),
                Recipes = this.recipesService.GetPersonalRecipes<RecipeInListViewModel>(sortOrder, page, itemsPerPage, user.Id),
                ControllerName = this.ControllerContext.ActionDescriptor.ControllerName,
                ActionName = this.ControllerContext.ActionDescriptor.ActionName,
                SortOrder = sortOrder,
            };

            if (page > viewModel.PagesCount && viewModel.PagesCount > 0)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(viewModel);
        }
    }
}
