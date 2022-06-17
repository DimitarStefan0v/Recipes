using RecipesApp.Core.Models;

namespace RecipesApp.Core.Contracts
{
    public interface IRecipesService
    {
        Task CreateAsync(RecipeInputModel input, string userId);

        IEnumerable<RecipeInListViewModel> GetAll(int page, int itemsPerPage = 12);

        IEnumerable<RecipeInListViewModel> GetRecentlyAddedRecipes(int count = 9);

        int GetCount();

        SingleRecipeViewModel GetById(int id);
    }
}
