namespace Recipes.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IDeletableEntityRepository<Vote> votesRepository;

        public VotesService(IDeletableEntityRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public async Task SetVoteAsync(int recipeId, string userId, byte value)
        {
            var vote = this.votesRepository
                .All()
                .Where(x => x.RecipeId == recipeId && x.AddedByUserId == userId)
                .FirstOrDefault();

            if (vote == null)
            {
                vote = new Vote
                {
                    RecipeId = recipeId,
                    AddedByUserId = userId,
                };

                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
