namespace Recipes.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Recipes.Web.ViewModels.Recipes;

    public interface IRecipesService
    {
        Task CreateAsync(CreateRecipeInputModel input, string userId);

        IEnumerable<T> GetAll<T>(string sort, int page, int itemsPerPage);

        IEnumerable<T> GetAllUnapproved<T>(int page, int itemsPerPage);

        IEnumerable<T> GetAllRecipesByName<T>(string search, string sort, int page, int itemsPerPage);

        IEnumerable<T> GetRecipesByCategoryId<T>(int categoryId, string sort, int page, int itemsPerPage);

        IEnumerable<T> GetPersonalRecipes<T>(string sort, int page, int itemsPerPage, string userId);

        T GetById<T>(int id);

        Task UpdateAsync(int id, EditRecipeInputModel input);

        Task DeleteAsync(int id);

        Task ApproveRecipe(int id);

        Task AddRecipeToFavoritesAsync(int recipeId, string userId);

        bool IsRecipeInUserFavorites(int recipeId, string userId);

        Task RemoveRecipeFromFavoritesAsync(int recipeId, string userId);

        IEnumerable<T> GetFavorites<T>(int page, int itemsPerPage, string userId);

        string GetAuhorId(int id);
    }
}
