namespace RecipesApp.Core.Models
{
    public class RecipesListViewModel : PagingViewModel
    {
        public IEnumerable<RecipeInListViewModel> Recipes { get; set; }

    }
}
