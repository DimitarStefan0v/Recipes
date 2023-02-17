namespace Recipes.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Recipes.Common;
    using Recipes.Data.Models;
    using Recipes.Services.Data.Contracts;
    using Recipes.Web.ViewModels.Comments;
    using Recipes.Web.ViewModels.Posts;

    public class PostsController : BaseController
    {
        private readonly IPostsService postsService;
        private readonly ICountsService countsService;
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(
            IPostsService postsService,
            ICountsService countsService,
            ICommentsService commentsService,
            UserManager<ApplicationUser> userManager)
        {
            this.postsService = postsService;
            this.countsService = countsService;
            this.commentsService = commentsService;
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

        public async Task<IActionResult> ById(int id, int page = 1)
        {
            await this.countsService.IncreasePostViews(id);

            if (id <= 0 || page <= 0)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var itemsPerPage = 5;

            var viewModel = this.postsService.GetById<SinglePostViewModel>(id);
            viewModel.Comments = this.commentsService.GetPostComments<CommentInListViewModel>(id, page, itemsPerPage);
            viewModel.ItemsPerPage = itemsPerPage;
            viewModel.PageNumber = page;
            viewModel.ItemsCount = this.countsService.GetCommentsCountByPostId(id);
            viewModel.ControllerName = this.ControllerContext.ActionDescriptor.ControllerName;
            viewModel.ActionName = this.ControllerContext.ActionDescriptor.ActionName;
            viewModel.SourceId = id;

            if (page > viewModel.PagesCount && viewModel.PagesCount > 0)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.postsService.DeleteAsync(id);
            return this.RedirectToAction("Index", "Forum", new { area = string.Empty });
        }
    }
}
