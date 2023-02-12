namespace Recipes.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    public class PostsListViewModel
    {
        public IEnumerable<PostInListViewModel> Posts { get; set; }
    }
}
