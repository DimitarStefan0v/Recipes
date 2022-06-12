using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipesApp.Core.Contracts;
using RecipesApp.Core.Models;
using System.Security.Claims;

namespace RecipesApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService _votesService)
        {
            votesService = _votesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostVoteResponseViewModel>> Post(PostVoteInputModel input)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await votesService.SetVoteAsync(input.RecipeId, userId, input.Value);

            var averageVotes = votesService.GetAverageVotes(input.RecipeId);
            var postVoteResponse = new PostVoteResponseViewModel
            {
                AverageVote = averageVotes
            };

            return postVoteResponse;
        }
    }
}
