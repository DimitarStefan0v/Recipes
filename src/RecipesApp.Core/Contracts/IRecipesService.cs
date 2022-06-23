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

        IEnumerable<RecipeInListViewModel> GetRecipesByIngredients(IEnumerable<int> ingredientIds);

        Task UpdateAsync(int id, EditRecipeInputModel input);
    }
}
