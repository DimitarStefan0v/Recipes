namespace Recipes.Web.ViewModels.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using global::Recipes.Common.Attributes;
    using global::Recipes.Common.Constants;
    using global::Recipes.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Http;

    public class CreateRecipeInputModel
    {
        [Display(Name = "Заглавие на рецептата")]
        [Required(ErrorMessage = RecipeErrorMessages.NameRequired)]
        [MinLength(3, ErrorMessage = RecipeErrorMessages.NameLength)]
        [MaxLength(50, ErrorMessage = RecipeErrorMessages.NameLength)]
        public string Name { get; set; }

        [Display(Name = "Начин на приготвяне")]
        [Required(ErrorMessage = RecipeErrorMessages.DescriptionRequired)]
        [MinLength(10, ErrorMessage = RecipeErrorMessages.DescriptionLength)]
        public string Description { get; set; }

        [Display(Name = "Време за приготвяне /минути/")]
        public int? PreparationTime { get; set; }

        [Display(Name = "Време за готвене /минути/")]
        public int? CookingTime { get; set; }

        [Display(Name = "Порции")]
        public int? PortionsCount { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        public ICollection<CategoriesViewModel> Categories { get; set; }

        [Required(ErrorMessage = RecipeErrorMessages.IngredientRequired)]
        public ICollection<IngredientInputModel> Ingredients { get; set; }

        [Display(Name = "Снимка")]
        [Required(ErrorMessage = RecipeErrorMessages.ImageRequired)]
        [MaxFileSize(4 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        public IFormFile Image { get; set; }
    }
}
