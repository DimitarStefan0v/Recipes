namespace Recipes.Web.ViewModels.Messages
{
    using System.Collections.Generic;

    public class MessagesListViewModel
    {
        public IEnumerable<MessageInListViewModel> Messages { get; set; }
    }
}
