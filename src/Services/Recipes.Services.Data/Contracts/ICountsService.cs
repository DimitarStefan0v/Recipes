namespace Recipes.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using Recipes.Web.ViewModels.Home;

    public interface ICountsService
    {
        StatsViewModel GetStats();

        Task IncreaseViews(int id, bool forCategory);

        Task IncreasePostViews(int id);

        int GetRecipesCount();

        int GetFavoriteRecipesCount(string userId);

        int GetRecipesCountByName(string search);

        int GetRecipesCountByCategoryId(int id);

        int GetUnapprovedRecipesCount();

        int GetPersonalRecipesCount(string userId);

        int GetMessagesCount();

        int GetPostsCount();

        int GetCommentsCountByPostId(int id);

        int GetUnapprovedPostsCount();

        int GetUnapprovedCommentsCount();
    }
}
