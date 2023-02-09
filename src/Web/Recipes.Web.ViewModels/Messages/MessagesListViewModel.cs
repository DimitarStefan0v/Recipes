namespace Recipes.Web.ViewModels.Messages
{
    using System.Collections.Generic;

    public class MessagesListViewModel : PagingViewModel
    {
        public IEnumerable<MessageInListViewModel> Messages { get; set; }
    }
}
