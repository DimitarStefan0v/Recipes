using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipesApp.Core.Constants;
using RecipesApp.Core.Contracts;
using RecipesApp.Core.Models;
using RecipesApp.Infrastructure.Data.Identity;
using System.Security.Claims;

namespace RecipesApp.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public RecipesController(IRecipesService _recipesService,
            ICategoriesService _categoriesService,
            UserManager<ApplicationUser> _userManager)
        {
            recipesService = _recipesService;
            categoriesService = _categoriesService;
            userManager = _userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new RecipeInputModel();
            viewModel.Categories = categoriesService.GetAllCategories();

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(RecipeInputModel input)
        {
            if (!ModelState.IsValid)
            {
                input.Categories = categoriesService.GetAllCategories();
                return View(input);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                await recipesService.CreateAsync(input, userId);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                input.Categories = categoriesService.GetAllCategories();
                return View(input);
            }

            return Redirect("/");
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            int itemPerPage = 12;

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = itemPerPage,
                PageNumber = id,
                RecipesCount = recipesService.GetCount(),
                Recipes = recipesService.GetAll(id, 12),
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ById(int id)
        {
            var viewModel = recipesService.GetById(id);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            viewModel.IsRecipeFavorite = recipesService.IsRecipeFavorite(userId, id);

            var user = await userManager.FindByNameAsync(viewModel.AddedByUser);
            viewModel.IsTakenFromRecipeGotvachWebsite = await userManager.IsInRoleAsync(user, Roles.Administrator);

            return View(viewModel);
        }

        public IActionResult ByName([FromQuery(Name = "searchterm")] string name, int id = 1)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            int itemPerPage = 12;

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = itemPerPage,
                PageNumber = id,
                Recipes = recipesService.GetRecipesByName(name, id, 12),
            };

            viewModel.RecipesCount = viewModel.Recipes.Count();

            ViewData["SearchString"] = name;

            return View(viewModel);
        }

        [Authorize(Roles = Roles.Administrator)]
        public IActionResult Edit(int id)
        {
            var recipe = recipesService.GetById(id);

            var recipeToEdit = new EditRecipeInputModel
            {
                Id = id,
                Name = recipe.Name,
                Instructions = recipe.Instructions,
                PreparationTime = (int)recipe.PreparationTime.Value.TotalMinutes,
                CookingTime = (int)recipe.CookingTime.Value.TotalMinutes,
                PortionsCount = recipe.PortionsCount,
            };

            recipeToEdit.Categories = categoriesService.GetAllCategories();

            return View(recipeToEdit);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRecipeInputModel recipe)
        {
            if (!ModelState.IsValid)
            {
                recipe.Id = id;
                recipe.Categories = categoriesService.GetAllCategories();
                return View(recipe);
            }

            await recipesService.UpdateAsync(id, recipe);

            return RedirectToAction(nameof(ById), new { id });
        }

        [HttpPost]
        [Authorize(Roles = Roles.Administrator)]

        public async Task<IActionResult> Delete(int id)
        {
            await recipesService.DeleteAsync(id);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public async Task<IActionResult> AddToFavorites(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await recipesService.AddFavoriteRecipeAsync(userId, id);

            return RedirectToAction(nameof(Favorites));
        }

        [Authorize]
        public async Task<IActionResult> Favorites(int page = 1)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (page <= 0)
            {
                return NotFound();
            }

            int itemPerPage = 12;

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = itemPerPage,
                PageNumber = page,
                Recipes = recipesService.GetFavoriteRecipes(userId, page)
            };

            if (viewModel.Recipes != null)
            {
                viewModel.RecipesCount = viewModel.Recipes.Count();
            }
            else
            {
                viewModel.RecipesCount = 0;
            }

            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> DeleteFromFavorites(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await recipesService.DeleteFavoriteRecipe(userId, id);
            return RedirectToAction(nameof(Favorites));
        }
    }
}
