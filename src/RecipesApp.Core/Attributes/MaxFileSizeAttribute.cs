using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using RecipesApp.Core.Constants;

namespace RecipesApp.Core.Attributes
{
    internal class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Максималния размер на картинката е {RecipeErrorMessages.MaxImageSize} мегабайта.";
        }
    }
}
