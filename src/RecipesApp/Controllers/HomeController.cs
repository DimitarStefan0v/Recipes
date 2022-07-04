using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RecipesApp.Core.Contracts;
using RecipesApp.Core.Models;
using RecipesApp.Models;
using System.Diagnostics;

namespace RecipesApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> logger;

        private readonly IDistributedCache cache;

        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;

        public HomeController(ILogger<HomeController> _logger,
            IDistributedCache _cache,
            IRecipesService _recipesService,
            ICategoriesService _categoriesService)
        {
            logger = _logger;
            cache = _cache;
            recipesService = _recipesService;
            categoriesService = _categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            //DateTime dateTime = DateTime.Now;
            //var cachedData = await cache.GetStringAsync("cachedTime");

            //if (cachedData == null)
            //{
            //    cachedData = dateTime.ToString();
            //    DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions()
            //    {
            //        SlidingExpiration = TimeSpan.FromSeconds(20),
            //        AbsoluteExpiration = DateTime.Now.AddSeconds(60)
            //    };

            //    await cache.SetStringAsync("cachedTime", cachedData, cacheOptions);
            //}

            //ViewBag.DateTime = cachedData;

            var viewModel = new HomeViewModel
            {
                RecentRecipes = recipesService.GetRecentRecipes(),
                MostVotedRecipes = recipesService.GetMostVotedRecipes(),
                MostCommentedRecipes = recipesService.GetMostCommentedRecipes(),
                Categories = categoriesService.GetCategoriesWithImages()
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}