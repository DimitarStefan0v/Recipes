namespace Recipes.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using global::Recipes.Web.ViewModels.Recipes;

    public class IndexViewModel
    {
        public IEnumerable<RecipeInListViewModel> RecentRecipes { get; set; }

        public IEnumerable<RecipeInListViewModel> MostPopularRecipes { get; set; }

        public IEnumerable<RecipeInListViewModel> MostCommentedRecipes { get; set; }
    }
}
