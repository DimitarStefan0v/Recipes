namespace Recipes.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Recipes.Common;
    using Recipes.Services.Data;
    using Recipes.Web.ViewModels.Messages;
    using Recipes.Web.ViewModels.Recipes;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdminController : Controller
    {
        private readonly ICountsService countsService;
        private readonly IRecipesService recipesService;
        private readonly IMessagesService messagesService;

        public AdminController(
            ICountsService countsService,
            IRecipesService recipesService,
            IMessagesService messagesService)
        {
            this.countsService = countsService;
            this.recipesService = recipesService;
            this.messagesService = messagesService;
        }

        public IActionResult AllUnapproved(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            int itemsPerPage = 1;

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                ItemsCount = this.countsService.GetUnapprovedRecipesCount(),
                Recipes = this.recipesService.GetAllUnapproved<RecipeInListViewModel>(id, itemsPerPage),
                ControllerName = this.ControllerContext.ActionDescriptor.ControllerName,
                ActionName = this.ControllerContext.ActionDescriptor.ActionName,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Approve(int id)
        {
            await this.recipesService.ApproveRecipe(id);
            return this.RedirectToAction(nameof(this.AllUnapproved), new { id = 1 });
        }

        public IActionResult Messages(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            int itemsPerPage = 2;

            var viewModel = new MessagesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                ItemsCount = this.countsService.GetMessagesCount(),
                Messages = this.messagesService.GetAll<MessageInListViewModel>(id, itemsPerPage),
                ControllerName = this.ControllerContext.ActionDescriptor.ControllerName,
                ActionName = this.ControllerContext.ActionDescriptor.ActionName,
            };

            return this.View(viewModel);
        }
    }
}
