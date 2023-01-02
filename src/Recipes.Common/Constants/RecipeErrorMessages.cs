namespace Recipes.Common.Constants
{
    public static class RecipeErrorMessages
    {
        public const string NameRequired = "Името на рецептата е задължително";

        public const string NameLength = "Името на рецептата трябва да е с дължина между 3 и 50 символа";

        public const string DescriptionRequired = "Начина на приготвяне е задължителен";

        public const string DescriptionLength = "Начина на приготвяне трябва да е с дължина минимум 10 символа";

        public const string IngredientRequired = "Съставки към рецептата са задължителни";

        public const string IngredientNameRequired = "Името на съставката е задължително";

        public const string IngredientNameLength = "Името на съставката трябва е с дължина между 2 и 50 символа";

        public const string QuantityRequired = "Количеството към съставката е задължително";

        public const string ImageRequired = "Снимка към рецептата е задължителна";

        public const string AllowedImageExtensions = "jpg, png, gif, jpeg";

        public const int MaxImageSize = 4;
    }
}
