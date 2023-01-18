using Microsoft.AspNetCore.Mvc;

namespace Recipes.Web.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
