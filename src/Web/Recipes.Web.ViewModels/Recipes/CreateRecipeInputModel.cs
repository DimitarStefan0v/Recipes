namespace Recipes.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CreateRecipeInputModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int? PreparationTime { get; set; }

        public int? CookingTime { get; set; }

        public int CategoryId { get; set; }
    }
}
