namespace RecipesApp.Core.Constants
{
    public static class RecipeErrorMessages
    {
        public const string RecipeNameRequired = "Името на рецептата е задължително";

        public const string RecipeNameLength = "Името на рецептата трябва да е с дължина между 3 и 50 символа";

        public const string RecipeInstructionsRequired = "Начина на приготвяне е задължителен";

        public const string RecipeInstructionsLength = "Начина на приготвяне трябва да е с дължина минимум 10 символа";

        public const string IngredientNameRequired = "Името на съставката e задължително";

        public const string IngredientNameLength = "Името на съставката трябва е с дължина между 2 и 20 символа";

        public const string QuantityNameRequired = "Количеството на съставката е задължително";

        public const string ImageRequired = "Снимка към рецепта е задължителна";

        public const int MaxImageSize = 3;

        public const string AllowedImageExtensions = "jpg, png, gif";

    }
}
