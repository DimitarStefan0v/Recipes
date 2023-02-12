namespace Recipes.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    public class PostsListViewModel : PagingViewModel
    {
        public IEnumerable<PostInListViewModel> Posts { get; set; }
    }
}
