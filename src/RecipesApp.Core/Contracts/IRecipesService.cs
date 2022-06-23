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

        IEnumerable<RecipeInListViewModel> GetRecipesByIngredients(int page, IEnumerable<int> ingredientIds, int itemsPerPage = 12);

        Task UpdateAsync(int id, EditRecipeInputModel input);
    }
}
