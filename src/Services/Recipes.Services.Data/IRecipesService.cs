namespace Recipes.Services.Data
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

        T GetById<T>(int id);

        Task UpdateAsync(int id, EditRecipeInputModel input);

        Task DeleteAsync(int id);

        Task ApproveRecipe(int id);

        Task AddRecipeToFavoritesAsync(int recipeId, string userId);

        bool IsRecipeInUserFavorites(int recipeId, string userId);
    }
}
