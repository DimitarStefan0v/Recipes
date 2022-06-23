using Microsoft.AspNetCore.Mvc;
using RecipesApp.Core.Contracts;
using RecipesApp.Core.Models;

namespace RecipesApp.Controllers
{
    public class SearchRecipesController : Controller
    {
        private readonly IIngredientsService ingredientsService;
        private readonly IRecipesService recipesService;

        public SearchRecipesController(IIngredientsService _ingredientsService, 
            IRecipesService _recipesService)
        {
            ingredientsService = _ingredientsService;
            recipesService = _recipesService;
        }

        public IActionResult Index()
        {
            var viewModel = new SearchIndexViewModel
            {
                Ingredients = ingredientsService.GetAll(),
            };

            return View(viewModel);
        }

        public IActionResult List(SearchListInputModel input)
        {
            var viewModel = new ListViewModel
            {
                Recipes = recipesService.GetRecipesByIngredients(input.Ingredients),
            };

            return View(viewModel);
        }
    }
}
