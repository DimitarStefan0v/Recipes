namespace Recipes.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Recipes.Common;
    using Recipes.Data.Models;
    using Recipes.Services.Data;
    using Recipes.Services.Messaging;
    using Recipes.Web.ViewModels;
    using Recipes.Web.ViewModels.Home;
    using Recipes.Web.ViewModels.Recipes;

    public class HomeController : BaseController
    {
        private readonly ICountsService countsService;
        private readonly IUsersService usersService;
        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;

        public HomeController(
            ICountsService countsService,
            IUsersService usersService,
            IRecipesService recipesService,
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            this.countsService = countsService;
            this.usersService = usersService;
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            viewModel = this.countsService.GetStats();
            viewModel.RecentRecipes = this.recipesService.GetAll<RecipeInListViewModel>("descending", 1, 3);
            viewModel.MostPopularRecipes = this.recipesService.GetAll<RecipeInListViewModel>("popularity", 1, 3);
            viewModel.Categories = this.categoriesService.GetCategories();

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult Contacts()
        {
            var viewModel = new ContactInputModel();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Contacts(ContactInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();

            string email = "dstefanov737@gmail.com";

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.usersService.CreateMessageAsync(input, user == null ? null : user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            await this.emailSender.SendEmailAsync(input.Email, input.Name, email, input.Title, input.Content);

            this.TempData["messageSended"] = "messageSended";
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
