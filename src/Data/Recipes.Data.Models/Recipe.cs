namespace Recipes.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Recipes.Data.Common.Models;

    public class Recipe : BaseDeletableModel<int>
    {
        public Recipe()
        {
            this.Ingredients = new HashSet<RecipeIngredient>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan? PreparationTime { get; set; }

        public TimeSpan? CookingTime { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<RecipeIngredient> Ingredients { get; set; }
    }
}
