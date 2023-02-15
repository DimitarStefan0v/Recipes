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

    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment(string input, int id, bool forPost)
        {
            if (!this.ModelState.IsValid || string.IsNullOrWhiteSpace(input))
            {
                if (forPost)
                {
                    return this.RedirectToAction("ById", "Posts", new { id });
                }
                else
                {
                    return this.RedirectToAction("ById", "Recipes", new { id });
                }
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                if (forPost)
                {
                    await this.commentsService.AddCommentAsync(id, input, user.Id, true);
                }
                else
                {
                    await this.commentsService.AddCommentAsync(id, input, user.Id, false);
                }
            }
            catch (Exception ex)
            {
            }

            if (forPost)
            {
                return this.RedirectToAction("ById", "Posts", new { id });
            }
            else
            {
                return this.RedirectToAction("ById", "Recipes", new { id });
            }
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteComment(int id)
        {
            await this.commentsService.DeleteAsync(id);
            return this.RedirectToAction("Index", "Home");
        }

        public IEnumerable<CommentInListViewModel> LoadComments(int id)
        {
            var comments = this.commentsService.GetAllPostComments<CommentInListViewModel>(id);
            return comments;
        }
    }
}
