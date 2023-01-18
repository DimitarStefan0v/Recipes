namespace Recipes.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Recipes.Common;
    using Recipes.Data.Models;
    using Recipes.Services.Data;
    using Recipes.Web.ViewModels.Categories;
    using Recipes.Web.ViewModels.Recipes;

    public class RecipesController : BaseController
    {
        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public RecipesController(IRecipesService recipesService, ICategoriesService categoriesService, UserManager<ApplicationUser> userManager)
        {
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateRecipeInputModel();
            viewModel.Categories = this.categoriesService.GetCategories<CategoryInListViewModel>();
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = this.categoriesService.GetCategories<CategoryInListViewModel>();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.recipesService.CreateAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.Categories = this.categoriesService.GetCategories<CategoryInListViewModel>();
                return this.View(input);
            }

            return this.Redirect("/");
        }

        public IActionResult All()
        {
            var viewModel = new RecipesListViewModel();
            viewModel.Recipes = this.recipesService.GetAll<RecipeInListViewModel>();
            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.recipesService.GetById<SingleRecipeViewModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.recipesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
