namespace Recipes.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Recipes.Services.Data.Contracts;
    using Recipes.Web.ViewModels.Posts;

    public class ForumController : BaseController
    {
        private readonly IPostsService postsService;
        private readonly ICountsService countsService;

        public ForumController(IPostsService postsService, ICountsService countsService)
        {
            this.postsService = postsService;
            this.countsService = countsService;
        }

        public IActionResult Index(string sortOrder = "descending", int page = 1)
        {
            if (page <= 0)
            {
                return this.RedirectToAction("Error", "Home");
            }

            int itemsPerPage = 5;

            var viewModel = new PostsListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
                ItemsCount = this.countsService.GetPostsCount(),
                Posts = this.postsService.GetAll<PostInListViewModel>(sortOrder, page, itemsPerPage),
                ControllerName = this.ControllerContext.ActionDescriptor.ControllerName,
                ActionName = this.ControllerContext.ActionDescriptor.ActionName,
                SortOrder = sortOrder,
            };

            if (page > viewModel.PagesCount && viewModel.PagesCount > 0)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(viewModel);
        }
    }
}
