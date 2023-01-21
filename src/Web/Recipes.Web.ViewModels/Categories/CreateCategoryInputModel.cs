namespace Recipes.Web.ViewModels.Categories
{
    using System.ComponentModel.DataAnnotations;

    using global::Recipes.Common.Attributes;
    using global::Recipes.Common.Constants;
    using Microsoft.AspNetCore.Http;

    public class CreateCategoryInputModel
    {
        [Display(Name = "Заглавие на категорията")]
        [Required(ErrorMessage = CategoryErrorMessages.NameRequired)]
        [MinLength(4, ErrorMessage = CategoryErrorMessages.NameLength)]
        [MaxLength(30, ErrorMessage = CategoryErrorMessages.NameLength)]
        public string Name { get; set; }

        [Display(Name = "Цвят на категорията")]
        public string Color { get; set; }

        [Display(Name = "Снимка на категорията")]
        [MaxFileSize(4 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        public IFormFile Image { get; set; }
    }
}
