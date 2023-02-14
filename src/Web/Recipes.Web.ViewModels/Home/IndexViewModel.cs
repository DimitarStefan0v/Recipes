namespace Recipes.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using global::Recipes.Web.ViewModels.Categories;
    using global::Recipes.Web.ViewModels.Recipes;

    public class IndexViewModel
    {
        public IEnumerable<RecipeInListViewModel> RecentRecipes { get; set; }

        public IEnumerable<RecipeInListViewModel> MostPopularRecipes { get; set; }

        public IEnumerable<CategoryInListViewModel> Categories { get; set; }

        public int UsersCount { get; set; }

        public int ApprovedRecipesCount { get; set; }

        public int WaitingForApprovalRecipesCount { get; set; }

        public int CategoriesCount { get; set; }

        public int IngredientsCount { get; set; }

        public int PostsCount { get; set; }

        public int CommentsForPostsCount { get; set; }

        public int CommentsForRecipesCount { get; set; }
    }
}
