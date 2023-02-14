namespace Recipes.Web.ViewModels.Comments
{
    using System.Collections.Generic;

    public class CommentsListViewModel : PagingViewModel
    {
        public IEnumerable<CommentInListViewModel> Comments { get; set; }
    }
}
