namespace Recipes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using Recipes.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Recipes = new HashSet<Recipe>();
        }

        public string Name { get; set; }

        public string Color { get; set; }

        public int ViewCount { get; set; }

        [ForeignKey(nameof(CloudImage))]
        public int? ImageId { get; set; }

        public CloudImage Image { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
