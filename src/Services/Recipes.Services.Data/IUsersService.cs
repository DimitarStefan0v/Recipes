namespace Recipes.Services.Data
{
    using System.Threading.Tasks;

    using Recipes.Web.ViewModels.Home;

    public interface IUsersService
    {
        Task CreateMessageAsync(ContactInputModel input, string userId);

        Task AddRecipeToFavoritesAsync(int recipeId, string userId);

        Task RemoveRecipeFromFavoritesAsync(int recipeId, string userId);

        bool IsRecipeInUserFavorites(int recipeId, string userId);
    }
}
