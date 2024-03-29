﻿namespace Recipes.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Recipes.Data.Models;
    using Recipes.Services.Data.Contracts;
    using Recipes.Web.ViewModels.Recipes;
    using Recipes.Web.ViewModels.Votes;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : Controller
    {
        private readonly IVotesService votesService;
        private readonly IRecipesService recipesService;
        private readonly UserManager<ApplicationUser> userManager;

        public VotesController(
            IVotesService votesService,
            IRecipesService recipesService,
            UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
            this.recipesService = recipesService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostVoteResponseViewModel>> Post(PostVoteInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.votesService.SetVoteAsync(input.RecipeId, user.Id, input.Value);

            var averageVotes = this.votesService.GetAverageVotes(input.RecipeId);
            var postVoteResponse = new PostVoteResponseViewModel
            {
                AverageVote = averageVotes,
                VotesCount = this.recipesService.GetById<SingleRecipeViewModel>(input.RecipeId).VotesCount,
            };

            return postVoteResponse;
        }
    }
}
