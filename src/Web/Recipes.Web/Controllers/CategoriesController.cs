namespace Recipes.Web.Controllers
{
    using System;
    using System.Collections;
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

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipesService recipesService;
        private readonly ICountsService countsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CategoriesController(
            ICategoriesService categoriesService,
            IRecipesService recipesService,
            ICountsService countsService,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.recipesService = recipesService;
            this.countsService = countsService;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            var viewModel = new CategoriesListViewModel();
            viewModel.Categories = this.categoriesService.GetCategories();
            return this.View(viewModel);
        }

        public async Task<IActionResult> ById(int categoryId, string sortOrder = "descending", int id = 1)
        {
            await this.countsService.IncreaseViews(categoryId, true);

            if (id <= 0)
            {
                return this.NotFound();
            }

            int itemsPerPage = 9;

            var viewModel = this.categoriesService.GetById<SingleCategoryViewModel>(categoryId);
            viewModel.ItemsPerPage = itemsPerPage;
            viewModel.PageNumber = id;
            viewModel.ItemsCount = this.countsService.GetRecipesCountByCategoryId(categoryId);
            viewModel.RecipesByCategoryId = this.recipesService.GetRecipesByCategoryId<RecipeInListViewModel>(categoryId, sortOrder, id, itemsPerPage);
            viewModel.ControllerName = this.ControllerContext.ActionDescriptor.ControllerName;
            viewModel.ActionName = this.ControllerContext.ActionDescriptor.ActionName;
            viewModel.CategoryId = categoryId;
            viewModel.SortOrder = sortOrder;
            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var inputModel = this.categoriesService.GetById<EditCategoryInputModel>(id);
            return this.View(inputModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.categoriesService.UpdateAsync(id, input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            var inputModel = new CreateCategoryInputModel();
            return this.View(inputModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.categoriesService.CreateAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.categoriesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
