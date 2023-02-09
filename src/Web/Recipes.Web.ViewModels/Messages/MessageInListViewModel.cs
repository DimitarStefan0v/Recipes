namespace Recipes.Web.ViewModels.Messages
{
    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;

    public class MessageInListViewModel : IMapFrom<Message>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string AddedByUserUserName { get; set; }

        public string Name { get; set; }

        public string Ip { get; set; }

        public string Email { get; set; }
    }
}
