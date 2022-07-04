using RecipesApp.Core.Models;

namespace RecipesApp.Core.Contracts
{
    public interface IRecipesService
    {
        int GetCount();

        Task CreateAsync(RecipeInputModel input, string userId);

        SingleRecipeViewModel GetById(int id);

        IEnumerable<RecipeInListViewModel> GetAll(int page, int itemsPerPage = 12);

        IEnumerable<RecipeInListViewModel> GetRecentRecipes(int count = 3);

        IEnumerable<RecipeInListViewModel> GetMostVotedRecipes(int count = 3);

        IEnumerable<RecipeInListViewModel> GetMostCommentedRecipes(int count = 3);

        IEnumerable<RecipeInListViewModel> GetRecipesByIngredients(int page, IEnumerable<int> ingredientIds, int itemsPerPage = 12);

        IEnumerable<RecipeInListViewModel> GetRecipesByName(string name, int page, int itemsPerPage = 12);

        IEnumerable<RecipeInListViewModel> GetRecipesByCategory(int id, int page, int itemsPerPage = 12);

        IEnumerable<RecipeInListViewModel> GetRecipesByCategory(string name, int page, int itemsPerPage = 12);

        Task AddFavoriteRecipeAsync(string userId, int recipeId);

        bool IsRecipeFavorite(string userId, int recipeId);

        IEnumerable<RecipeInListViewModel> GetFavoriteRecipes(string userId, int page, int itemsPerPage = 12);

        Task UpdateAsync(int id, EditRecipeInputModel input);

        Task DeleteAsync(int id);

        Task DeleteFavoriteRecipe(string userId, int recipeId);
    }
}
