using Microsoft.AspNetCore.Mvc;

namespace RecipesApp.Controllers
{
    public class SearchRecipesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
