using RecipesApp.Core.Models;

namespace RecipesApp.Core.Contracts
{
    public interface IRecipesService
    {
        Task CreateAsync(RecipeInputModel input, string userId);

        IEnumerable<RecipeInListViewModel> GetAll(int page, int itemsPerPage = 12);

        IEnumerable<RecipeInListViewModel> GetRecentRecipes(int count = 3);

        IEnumerable<RecipeInListViewModel> GetMostVotedRecipes(int count = 3);

        int GetCount();

        SingleRecipeViewModel GetById(int id);

        Task UpdateAsync(int id, EditRecipeInputModel input);
    }
}
