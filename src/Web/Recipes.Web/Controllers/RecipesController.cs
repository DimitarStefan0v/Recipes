namespace Recipes.Web.Controllers
{
    using System;
    using System.Linq;
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
        private readonly IVotesService votesService;
        private readonly UserManager<ApplicationUser> userManager;

        public RecipesController(
            IRecipesService recipesService,
            ICategoriesService categoriesService,
            IVotesService votesService,
            UserManager<ApplicationUser> userManager)
        {
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
            this.votesService = votesService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateRecipeInputModel();
            viewModel.Categories = this.categoriesService.GetCategoryNames<CategoriesViewModel>();
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = this.categoriesService.GetCategoryNames<CategoriesViewModel>();
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
                input.Categories = this.categoriesService.GetCategoryNames<CategoriesViewModel>();
                return this.View(input);
            }

            return this.Redirect("/");
        }

        public IActionResult All(int id = 1)
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
                ItemsCount = this.recipesService.GetRecipesCount(),
                Recipes = this.recipesService.GetAll<RecipeInListViewModel>(id, itemsPerPage),
            };

            // Add ControllerName and ActionName so Paging can be extracted in PartialView
            // and work for different actions without repeating Paging in every view that needs it
            viewModel.ControllerName = this.ControllerContext.ActionDescriptor.ControllerName;
            viewModel.ActionName = this.ControllerContext.ActionDescriptor.ActionName;

            return this.View(viewModel);
        }

        public IActionResult GetAllByName([FromQuery(Name = "search")]string name, int id = 1)
        {
            if (string.IsNullOrWhiteSpace(name) || id <= 0)
            {
                return this.NotFound();
            }

            int itemsPerPage = 1;

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                ItemsCount = this.recipesService.GetRecipesCountByName(name),
                Recipes = this.recipesService.GetAllRecipesByName<RecipeInListViewModel>(name, id, itemsPerPage),
            };

            this.ViewData["SearchString"] = name.Trim();
            viewModel.ControllerName = this.ControllerContext.ActionDescriptor.ControllerName;
            viewModel.ActionName = this.ControllerContext.ActionDescriptor.ActionName;
            viewModel.Search = name.Trim();

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.recipesService.GetById<SingleRecipeViewModel>(id);
            viewModel.AverageVotesValue = this.votesService.GetAverageVotes(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.recipesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var inputModel = this.recipesService.GetById<EditRecipeInputModel>(id);
            return this.View(inputModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRecipeInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                await this.recipesService.UpdateAsync(id, inputModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(inputModel);
            }

            return this.RedirectToAction(nameof(this.ById), new { id });
        }
    }
}
