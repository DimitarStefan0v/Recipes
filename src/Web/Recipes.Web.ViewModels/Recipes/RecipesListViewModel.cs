namespace Recipes.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    public class RecipesListViewModel
    {
        public IEnumerable<RecipeInListViewModel> Recipes { get; set; }
    }
}
