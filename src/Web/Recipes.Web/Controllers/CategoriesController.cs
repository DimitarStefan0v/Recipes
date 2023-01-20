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

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CategoriesController(
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            var viewModel = new CategoriesListViewModel();
            viewModel.Categories = this.categoriesService.GetCategories();
            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.categoriesService.GetById<SingleCategoryViewModel>(id);
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

            return this.RedirectToAction(nameof(this.ById), new { id });
        }
    }
}
