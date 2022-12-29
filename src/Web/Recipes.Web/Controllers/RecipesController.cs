using Microsoft.AspNetCore.Mvc;

namespace Recipes.Web.Controllers
{
    public class RecipesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
