namespace Recipes.Data.Models
{
    using Recipes.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public int? RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public int? PostId { get; set; }

        public Post Post { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
