using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipesApp.Core.Constants;
using RecipesApp.Core.Contracts;
using RecipesApp.Core.Models;
using System.Security.Claims;

namespace RecipesApp.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService _commentsService)
        {
            commentsService = _commentsService;
        }


        [Authorize]
        public IActionResult Add(int id)
        {
            var inputModel = new CommentInputModel();
            inputModel.RecipeId = id;
            return View(inputModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(int id, CommentInputModel input)
        {
            input.RecipeId = id;

            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                await commentsService.AddAsync(input, userId);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(input);
            }

            return RedirectToAction("ById", "Recipes", new { id = id });
        }

        [HttpPost]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Delete(int id, int recipeId)
        {
            await commentsService.DeleteAsync(id);

            return RedirectToAction("ById", "Recipes", new { id = recipeId });
        }   
    }
}
