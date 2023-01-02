namespace Recipes.Common.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    using Microsoft.AspNetCore.Http;
    using Recipes.Web.ViewModels.Constants;

    internal class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            this.extensions = extensions;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!this.extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(this.GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Позволените разширения за файл са: {RecipeErrorMessages.AllowedImageExtensions}";
        }
    }
}
