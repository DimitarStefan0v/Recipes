﻿namespace Recipes.Web.ViewModels.Recipes
{
    using System.ComponentModel.DataAnnotations;

    using global::Recipes.Web.ViewModels.Constants;

    public class IngredientInputModel
    {
        [Required(ErrorMessage = RecipeErrorMessages.IngredientNameRequired)]
        [MinLength(2, ErrorMessage = RecipeErrorMessages.IngredientNameLength)]
        [MaxLength(50, ErrorMessage = RecipeErrorMessages.IngredientNameLength)]
        public string IngredientName { get; set; }

        [Required(ErrorMessage = RecipeErrorMessages.QuantityRequired)]
        public string Quantity { get; set; }
    }
}
