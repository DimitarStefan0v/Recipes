using Microsoft.AspNetCore.Mvc;
using RecipesApp.Core.Models;

namespace RecipesApp.Controllers
{
    public class RecipeController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RecipeInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            return Json(input);
        }
    }
}
