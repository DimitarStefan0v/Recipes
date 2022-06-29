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
            return View();
        }

        public IActionResult ById(int id, int pageNumber = 1)
        {
            if (pageNumber <= 0)
            {
                return NotFound();
            }

            int itemPerPage = 12;

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = itemPerPage,
                PageNumber = pageNumber,
                Recipes = recipesService.GetRecipesByCategory(id, pageNumber, 12),
            };

            viewModel.RecipesCount = viewModel.Recipes.Count();
            var category = categoriesService.GetAllCategories().Where(x => x.Id == id).FirstOrDefault();
            TempData["CategoryName"] = category.Name;

            return View(viewModel);
        }

        public IActionResult ByName(string name, int pageNumber = 1)
        {
            if (pageNumber <= 0)
            {
                return NotFound();
            }

            int itemPerPage = 12;

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = itemPerPage,
                PageNumber = pageNumber,
                Recipes = recipesService.GetRecipesByCategory(name, pageNumber, 12),
            };

            viewModel.RecipesCount = viewModel.Recipes.Count();

            return View(viewModel);
        }
    }
}
