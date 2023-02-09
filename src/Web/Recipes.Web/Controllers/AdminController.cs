namespace Recipes.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Recipes.Common;
    using Recipes.Services.Data;
    using Recipes.Web.ViewModels.Recipes;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdminController : Controller
    {
        private readonly ICountsService countsService;
        private readonly IRecipesService recipesService;

        public AdminController(ICountsService countsService, IRecipesService recipesService)
        {
            this.countsService = countsService;
            this.recipesService = recipesService;
        }

        public IActionResult AllUnapproved(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            int itemsPerPage = 1;

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                ItemsCount = this.countsService.GetUnapprovedRecipesCount(),
                Recipes = this.recipesService.GetAllUnapproved<RecipeInListViewModel>(id, itemsPerPage),
            };

            viewModel.ControllerName = this.ControllerContext.ActionDescriptor.ControllerName;
            viewModel.ActionName = this.ControllerContext.ActionDescriptor.ActionName;

            return this.View(viewModel);
        }

        public async Task<IActionResult> Approve(int id)
        {
            await this.recipesService.ApproveRecipe(id);
            return this.RedirectToAction(nameof(this.AllUnapproved), new { id = 1 });
        }
    }
}
