﻿namespace Recipes.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Recipes.Common;
    using Recipes.Data.Models;
    using Recipes.Services.Data.Contracts;
    using Recipes.Web.ViewModels.Categories;
    using Recipes.Web.ViewModels.Recipes;

    public class RecipesController : BaseController
    {
        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;
        private readonly IVotesService votesService;
        private readonly ICountsService countsService;
        private readonly UserManager<ApplicationUser> userManager;

        public RecipesController(
            IRecipesService recipesService,
            ICategoriesService categoriesService,
            IVotesService votesService,
            ICountsService countsService,
            UserManager<ApplicationUser> userManager)
        {
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
            this.votesService = votesService;
            this.countsService = countsService;
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

            this.TempData["recipeCreated"] = "recipe created successfully";

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult All(string sortOrder = "descending", int page = 1)
        {
            if (page <= 0)
            {
                return this.RedirectToAction("Error", "Home");
            }

            int itemsPerPage = 9;

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
                ItemsCount = this.countsService.GetRecipesCount(),
                Recipes = this.recipesService.GetAll<RecipeInListViewModel>(sortOrder, page, itemsPerPage),
            };

            // Add ControllerName and ActionName so Paging can be extracted in PartialView
            // and work for different actions without repeating Paging in every view that needs it
            viewModel.ControllerName = this.ControllerContext.ActionDescriptor.ControllerName;
            viewModel.ActionName = this.ControllerContext.ActionDescriptor.ActionName;
            viewModel.SortOrder = sortOrder;

            if (page > viewModel.PagesCount && viewModel.PagesCount > 0)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(viewModel);
        }

        public IActionResult AllByName([FromQuery(Name = "search")] string name, string sortOrder = "descending", int page = 1)
        {
            if (string.IsNullOrWhiteSpace(name) || page <= 0)
            {
                return this.RedirectToAction("Error", "Home");
            }

            int itemsPerPage = 9;

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
                ItemsCount = this.countsService.GetRecipesCountByName(name),
                Recipes = this.recipesService.GetAllRecipesByName<RecipeInListViewModel>(name, sortOrder, page, itemsPerPage),
                ControllerName = this.ControllerContext.ActionDescriptor.ControllerName,
                ActionName = this.ControllerContext.ActionDescriptor.ActionName,
                Search = name.Trim(),
                SortOrder = sortOrder,
            };

            this.ViewData["SearchString"] = name.Trim();

            if (page > viewModel.PagesCount && viewModel.PagesCount > 0)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> ById(int id)
        {
            await this.countsService.IncreaseViews(id, false);

            var viewModel = this.recipesService.GetById<SingleRecipeViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            viewModel.AverageVotesValue = this.votesService.GetAverageVotes(id);

            var user = await this.userManager.GetUserAsync(this.User);

            viewModel.IsRecipeInFavorites = this.recipesService.IsRecipeInUserFavorites(viewModel.Id, user == null ? null : user.Id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var authorId = this.recipesService.GetAuhorId(id);

            var isUserAdmin = await this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName);

            if (user.Id == authorId || isUserAdmin == true)
            {
                await this.recipesService.DeleteAsync(id);
                this.TempData["recipeDeleted"] = true;
                return this.RedirectToAction(nameof(this.All));
            }

            this.TempData["forbiddenAccessForThisUser"] = true;
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var authorId = this.recipesService.GetAuhorId(id);

            var isUserAdmin = await this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName);

            if (user.Id == authorId || isUserAdmin == true)
            {
                var inputModel = this.recipesService.GetById<EditRecipeInputModel>(id);
                return this.View(inputModel);
            }

            this.TempData["forbiddenAccessForThisUser"] = true;
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [Authorize]
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

            this.TempData["recipeEdited"] = true;
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
