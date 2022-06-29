using Microsoft.AspNetCore.Mvc;

namespace RecipesApp.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ById(int id)
        {
            return View();
        }
    }
}
