namespace Recipes.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using global::Recipes.Web.ViewModels.Recipes;

    public class IndexViewModel
    {
        public IEnumerable<RecipeInListViewModel> RecentRecipes { get; set; }

        public IEnumerable<RecipeInListViewModel> MostPopularRecipes { get; set; }

        public int UsersCount { get; set; }

        public int ApprovedRecipesCount { get; set; }

        public int WaitingForApprovalRecipesCount { get; set; }

        public int CategoriesCount { get; set; }

        public int IngredientsCount { get; set; }
    }
}
