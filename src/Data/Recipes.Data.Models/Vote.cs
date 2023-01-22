namespace Recipes.Data.Models
{
    using Recipes.Data.Common.Models;

    public class Vote : BaseDeletableModel<int>
    {
        public byte Value { get; set; }

        public int RecipeId { get; set; }

        public int Recipe { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
