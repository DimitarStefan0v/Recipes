namespace Recipes.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Recipes.Data.Common.Models;

    public class CloudImage : BaseDeletableModel<int>
    {
        public string PicturePublicId { get; set; }

        public string PictureUrl { get; set; }

        [ForeignKey(nameof(Recipe))]
        public int? RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        [ForeignKey(nameof(Category))]
        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
