namespace Recipes.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using Recipes.Data.Common.Models;

    public class Recipe : BaseDeletableModel<int>
    {
        public Recipe()
        {
            this.Ingredients = new HashSet<RecipeIngredient>();
            this.Votes = new HashSet<Vote>();
            this.FavoriteRecipes = new HashSet<FavoriteRecipe>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan? PreparationTime { get; set; }

        public TimeSpan? CookingTime { get; set; }

        public int? PortionsCount { get; set; }

        public bool IsApproved { get; set; }

        public int ViewsCount { get; set; }

        [ForeignKey(nameof(CloudImage))]
        public int? ImageId { get; set; }

        public CloudImage Image { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public ICollection<RecipeIngredient> Ingredients { get; set; }

        public ICollection<Vote> Votes { get; set; }

        public ICollection<FavoriteRecipe> FavoriteRecipes { get; set; }
    }
}
