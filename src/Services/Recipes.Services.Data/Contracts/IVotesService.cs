namespace Recipes.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task SetVoteAsync(int recipeId, string userId, byte value);

        double GetAverageVotes(int recipeId);
    }
}
