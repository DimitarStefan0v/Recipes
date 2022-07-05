using Microsoft.AspNetCore.Mvc;
using RecipesApp.Core.Contracts;
using RecipesApp.Core.Models;

namespace RecipesApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;

        public CategoriesController(IRecipesService _recipesService,
            ICategoriesService _categoriesService)
        {
            recipesService = _recipesService;
            categoriesService = _categoriesService;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                Categories = categoriesService.GetCategoriesWithImages()
            };

            return View(viewModel);
        }

        public IActionResult ById(int categoryId, int pageNumber = 1)
        {
            if (pageNumber <= 0 || categoryId == 0)
            {
                return NotFound();
            }

            int itemPerPage = 12;

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = itemPerPage,
                PageNumber = pageNumber,
                RecipesCount = (int)recipesService.GetRecipesCountByCategory(categoryId),
                Recipes = recipesService.GetRecipesByCategory(categoryId, pageNumber, 12),
                FromCategoriesController = true,
            };

            var category = categoriesService.GetCategoriesWithImages()
                .Where(x => x.Id == categoryId)
                .FirstOrDefault();

            if (category != null)
            {
                TempData["CategoryId"] = category.Id;
                TempData["CategoryName"] = category.Name;
                TempData["CategoryImage"] = category.ImgUrl;
            }

            return View(viewModel);
        }

        public IActionResult ByName(string name)
        {
            int categoryId = categoriesService.GetCategoryIdByName(name);

            return RedirectToAction(nameof(ById), new { categoryId });
        }
    }
}
