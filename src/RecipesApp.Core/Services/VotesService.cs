using RecipesApp.Core.Contracts;
using RecipesApp.Infrastructure.Data;
using RecipesApp.Infrastructure.Data.Repositories;

namespace RecipesApp.Core.Services
{
    public class VotesService : IVotesService
    {
        private readonly IApplicationDbRepository repo;

        public VotesService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public double GetAverageVotes(int recipeId)
        {
            var votes = repo.All<Vote>()
                .Where(x => x.RecipeId == recipeId).
                ToList();

            if (votes.Count > 0)
            {
                return votes.Average(x => x.Value);
            }

            return 0;
        }

        public async Task SetVoteAsync(int recipeId, string userId, byte value)
        {
            var vote = repo.All<Vote>()
                .Where(x => x.UserId == userId && x.RecipeId == recipeId)
                .FirstOrDefault();

            if (vote == null)
            {
                vote = new Vote
                {
                    RecipeId = recipeId,
                    UserId = userId,
                };

                await repo.AddAsync(vote);
            }

            vote.Value = value;

            await repo.SaveChangesAsync();
        }
    }
}
