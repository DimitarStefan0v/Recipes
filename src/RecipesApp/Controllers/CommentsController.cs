using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Add()
        {
            var inputModel = new CommentInputModel();
            return View(inputModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(CommentInputModel input)
        {
            if(!ModelState.IsValid)
            {
                return View(input);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                commentsService.AddAsync(input, userId);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(input);
            }

            return Redirect("/");
        }
    }
}
