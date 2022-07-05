using RecipesApp.Core.Models;

namespace RecipesApp.Core.Contracts
{
    public interface IIngredientsService
    {
        IEnumerable<IngredientNameIdViewModel> GetAll();

        string GetIngredientNamesById(IList<int> ingredientIds);
    }
}
