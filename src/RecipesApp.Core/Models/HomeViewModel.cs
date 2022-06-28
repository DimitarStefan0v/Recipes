namespace RecipesApp.Core.Models
{
    public class HomeViewModel
    {
        public IEnumerable<RecipeInListViewModel> RecentRecipes { get; set; }

        public IEnumerable<RecipeInListViewModel> MostVotedRecipes { get; set; }

       // public IEnumerable<RecipeInListViewModel> MostCommentedRecipes { get; set; }

        public IEnumerable<IndexCategoryViewModel> Categories { get; set; }
    }
}
