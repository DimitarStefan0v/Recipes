using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipesApp.Core.Contracts;
using RecipesApp.Core.Models;
using RecipesApp.Infrastructure.Data.Identity;
using System.Security.Claims;

namespace RecipesApp.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public RecipeController(IRecipesService _recipesService,
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

        public IActionResult All(int id)
        {
            return View();
        }
    }
}
