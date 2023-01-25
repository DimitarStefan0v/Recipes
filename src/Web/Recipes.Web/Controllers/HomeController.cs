namespace Recipes.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Recipes.Common.Constants;
    using Recipes.Data.Models;
    using Recipes.Services.Data;
    using Recipes.Web.ViewModels;
    using Recipes.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly ICountsService countsService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(
            ICountsService countsService,
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.countsService = countsService;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexStatsViewModel();
            viewModel = this.countsService.GetStats();

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

            if (user == null && input.Name == null)
            {
                this.ModelState.AddModelError(string.Empty, ContactErrorMessages.NameRequired);
                return this.View(input);
            }

            if (user == null && input.Email == null)
            {
                this.ModelState.AddModelError(string.Empty, ContactErrorMessages.EmailRequired);
                return this.View(input);
            }

            if (input.Name == null || input.Email == null)
            {
                input.Name = user == null ? "Анонимно име" : user.UserName;
                input.Email = user == null ? "Aнонимен имейл" : user.Email;
            }

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

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
