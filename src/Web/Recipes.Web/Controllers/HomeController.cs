namespace Recipes.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Recipes.Data.Models;
    using Recipes.Services.Data.Contracts;
    using Recipes.Services.Messaging;
    using Recipes.Web.Infrastructure;
    using Recipes.Web.ViewModels;
    using Recipes.Web.ViewModels.Home;
    using Recipes.Web.ViewModels.Recipes;

    public class HomeController : BaseController
    {
        private readonly ICountsService countsService;
        private readonly IMessagesService messagesService;
        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;
        private readonly GoogleCaptchaService captchaService;

        public HomeController(
            ICountsService countsService,
            IMessagesService messagesService,
            IRecipesService recipesService,
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            GoogleCaptchaService captchaService)
        {
            this.countsService = countsService;
            this.messagesService = messagesService;
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.captchaService = captchaService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            viewModel = this.countsService.GetStatsForIndex();
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
            var captchaResult = await this.captchaService.VerifyToken(input.Token);
            if (!captchaResult)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            string email = "dstefanov737@gmail.com";

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.messagesService.CreateMessageAsync(input, user == null ? null : user.Id);
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
