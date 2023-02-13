namespace Recipes.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Recipes.Data.Models;
    using Recipes.Services.Data.Contracts;
    using Recipes.Web.ViewModels.Posts;

    public class PostsController : BaseController
    {
        private readonly IPostsService postsService;
        private readonly ICountsService countsService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(
            IPostsService postsService,
            ICountsService countsService,
            UserManager<ApplicationUser> userManager)
        {
            this.postsService = postsService;
            this.countsService = countsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreatePostInputModel();
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]

        public async Task<IActionResult> Create(CreatePostInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.postsService.CreateAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction("Index", "Forum", new { area = string.Empty });
        }

        public async Task<IActionResult> ById(int id)
        {
            await this.countsService.IncreasePostViews(id);

            if (id <= 0)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var viewModel = this.postsService.GetById<SinglePostViewModel>(id);

            return this.View(viewModel);
        }
    }
}
