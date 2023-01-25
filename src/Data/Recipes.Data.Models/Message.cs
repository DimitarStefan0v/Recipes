namespace Recipes.Data.Models
{
    using Recipes.Data.Common.Models;

    public class Message : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
