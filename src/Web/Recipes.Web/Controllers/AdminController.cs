namespace Recipes.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Data.SqlClient;
    using Recipes.Common;
    using Recipes.Services.Data.Contracts;
    using Recipes.Web.ViewModels.Comments;
    using Recipes.Web.ViewModels.Messages;
    using Recipes.Web.ViewModels.Posts;
    using Recipes.Web.ViewModels.Recipes;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdminController : Controller
    {
        private readonly ICountsService countsService;
        private readonly IRecipesService recipesService;
        private readonly IMessagesService messagesService;
        private readonly IPostsService postsService;
        private readonly ICommentsService commentsService;

        public AdminController(
            ICountsService countsService,
            IRecipesService recipesService,
            IMessagesService messagesService,
            IPostsService postsService,
            ICommentsService commentsService)
        {
            this.countsService = countsService;
            this.recipesService = recipesService;
            this.messagesService = messagesService;
            this.postsService = postsService;
            this.commentsService = commentsService;
        }

        public IActionResult AllUnapprovedRecipes(int page = 1)
        {
            if (page <= 0)
            {
                return this.NotFound();
            }

            int itemsPerPage = 9;

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
                ItemsCount = this.countsService.GetUnapprovedRecipesCount(),
                Recipes = this.recipesService.GetAllUnapproved<RecipeInListViewModel>(page, itemsPerPage),
                ControllerName = this.ControllerContext.ActionDescriptor.ControllerName,
                ActionName = this.ControllerContext.ActionDescriptor.ActionName,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Approve(int id, bool recipe, bool post, bool comment)
        {
            if (recipe)
            {
                await this.recipesService.ApproveRecipe(id);
            }
            else if (post)
            {
                await this.postsService.Approve(id);
            }
            else if (comment)
            {
                await this.commentsService.Approve(id);
            }

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult AllUnapprovedPosts(string sortOrder = "descending", int page = 1)
        {
            if (page <= 0)
            {
                return this.NotFound();
            }

            int itemsPerPage = 10;

            var viewModel = new PostsListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
                ItemsCount = this.countsService.GetUnapprovedPostsCount(),
                Posts = this.postsService.GetAllUnapproved<PostInListViewModel>(sortOrder, page, itemsPerPage),
                ControllerName = this.ControllerContext.ActionDescriptor.ControllerName,
                ActionName = this.ControllerContext.ActionDescriptor.ActionName,
                SortOrder = sortOrder,
            };

            return this.View(viewModel);
        }

        public IActionResult AllUnapprovedComments(int page = 1)
        {
            if (page <= 0)
            {
                return this.NotFound();
            }

            int itemsPerPage = 10;

            var viewModel = new CommentsListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
                ItemsCount = this.countsService.GetUnapprovedCommentsCount(),
                Comments = this.commentsService.GetAllUnapproved<CommentInListViewModel>(page, itemsPerPage),
                ControllerName = this.ControllerContext.ActionDescriptor.ControllerName,
                ActionName = this.ControllerContext.ActionDescriptor.ActionName,
            };

            return this.View(viewModel);
        }

        public IActionResult Messages(int page = 1)
        {
            if (page <= 0)
            {
                return this.NotFound();
            }

            int itemsPerPage = 5;

            var viewModel = new MessagesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
                ItemsCount = this.countsService.GetMessagesCount(),
                Messages = this.messagesService.GetAll<MessageInListViewModel>(page, itemsPerPage),
                ControllerName = this.ControllerContext.ActionDescriptor.ControllerName,
                ActionName = this.ControllerContext.ActionDescriptor.ActionName,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> DeleteMessage(int id)
        {
            await this.messagesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Messages), new { id = 1 });
        }
    }
}
