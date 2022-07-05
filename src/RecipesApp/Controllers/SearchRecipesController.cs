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

        public IActionResult List(SearchListInputModel input, int id = 1)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            int itemPerPage = 12;


            var viewModel = new RecipesListViewModel
            {
                Recipes = recipesService.GetRecipesByIngredients(id, input.Ingredients, 12),
                ItemsPerPage = itemPerPage,
                PageNumber = id
            };

            viewModel.RecipesCount = viewModel.Recipes.Count();

            ViewData["ingredients"] = ingredientsService.GetIngredientNamesById((IList<int>)input.Ingredients);

            return View(viewModel);
        }
    }
}
