namespace Recipes.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    public class CreateRecipeInputModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int? PreparationTime { get; set; }

        public int? CookingTime { get; set; }

        public int? PortionsCount { get; set; }

        public int CategoryId { get; set; }

        public ICollection<CategoriesViewModel> Categories { get; set; }

        public ICollection<IngredientInputModel> Ingredients { get; set; }
    }
}
