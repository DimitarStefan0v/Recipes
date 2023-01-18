namespace Recipes.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Recipes.Services.Data;
    using Recipes.Web.ViewModels.Categories;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult All()
        {
            var viewModel = this.categoriesService.GetCategories();
            return this.View(viewModel);
        }
    }
}
