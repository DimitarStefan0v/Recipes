using RecipesApp.Core.Models;

namespace RecipesApp.Core.Contracts
{
    public interface IRecipesService
    {
        Task CreateAsync(RecipeInputModel input, string userId);
    }
}
