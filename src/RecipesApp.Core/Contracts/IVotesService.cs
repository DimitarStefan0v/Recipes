namespace RecipesApp.Core.Contracts
{
    public interface IVotesService
    {
        Task SetVoteAsync(int recipeId, string userId, byte value);

        double GetAverageVotes(int recipeId);
    }
}
