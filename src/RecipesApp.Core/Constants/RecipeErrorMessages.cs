using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesApp.Core.Constants
{
    public static class RecipeErrorMessages
    {
        public const string RecipeNameRequired = "Името на рецептата е задължително";

        public const string RecipeNameLength = "Името на рецептата трябва да е с дължина между 3 и 15 символа";

        public const string RecipeInstructionsRequired = "Начина на приготвяне е задължителен";

        public const string RecipeInstructionsLength = "Начина на приготвяне трябва да е с дължина минимум 10 символа";

        public const string IngredientNameRequired = "Името на съставката e задължително";

        public const string IngredientNameLength = "Името на съставката трябва е с дължина между 2 и 10 символа";

        public const string IngredientQuantityRequired = "Количеството на съставката e задължително";

        public const string IngredientQuantityLength = "Количеството на съставката трябва е с дължина между 1 и 15 символа";
    }
}
