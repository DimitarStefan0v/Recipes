namespace Recipes.Web.ViewModels.Categories
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using global::Recipes.Common.Attributes;
    using global::Recipes.Common.Constants;
    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class EditCategoryInputModel : IMapFrom<Category>, IHaveCustomMappings
    {
        [Display(Name = "Заглавие на категорията")]
        [Required(ErrorMessage = CategoryErrorMessages.NameRequired)]
        [MinLength(4, ErrorMessage = CategoryErrorMessages.NameLength)]
        [MaxLength(30, ErrorMessage = CategoryErrorMessages.NameLength)]
        public string Name { get; set; }

        [Display(Name = "Цвят на категорията")]
        public string Color { get; set; }

        [Display(Name = "Текуща снимка на категорията")]
        public string ImageUrl { get; set; }

        [Display(Name = "Смяна на снимката")]
        [MaxFileSize(4 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        public IFormFile NewImage { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Category, EditCategoryInputModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(c => c.Image.PictureUrl));
        }
    }
}
