namespace Recipes.Web.ViewModels.Home
{
    public class StatsViewModel
    {
        public int UsersCount { get; set; }

        public int ApprovedRecipesCount { get; set; }

        public int WaitingForApprovalRecipesCount { get; set; }

        public int CategoriesCount { get; set; }

        public int PostsCount { get; set; }

        public int CommentsForPostsCount { get; set; }

        public int CommentsForRecipesCount { get; set; }
    }
}
