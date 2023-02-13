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

        public IActionResult Index(string sortOrder = "descending", int id = 1)
        {
            if (id <= 0)
            {
                return this.RedirectToAction("Error", "Home");
            }

            int itemsPerPage = 5;

            var viewModel = new PostsListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                ItemsCount = this.countsService.GetPostsCount(),
                Posts = this.postsService.GetAll<PostInListViewModel>(sortOrder, id, itemsPerPage),
                ControllerName = this.ControllerContext.ActionDescriptor.ControllerName,
                ActionName = this.ControllerContext.ActionDescriptor.ActionName,
                SortOrder = sortOrder,
            };

            return this.View(viewModel);
        }
    }
}
