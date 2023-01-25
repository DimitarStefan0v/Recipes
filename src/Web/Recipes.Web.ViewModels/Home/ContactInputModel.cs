namespace Recipes.Web.ViewModels.Home
{
    using System.ComponentModel.DataAnnotations;

    using global::Recipes.Common.Constants;

    public class ContactInputModel
    {
        [Display(Name = "Вашето име")]
        [MinLength(3, ErrorMessage = ContactErrorMessages.NameLength)]
        public string Name { get; set; }

        [Display(Name = "Вашият email адрес")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Заглавие на съобщението")]
        [Required(ErrorMessage = ContactErrorMessages.TitleRequired)]
        [MinLength(5, ErrorMessage = ContactErrorMessages.TitleLength)]
        public string Title { get; set; }

        [Display(Name = "Съдържание на съобщението")]
        [Required(ErrorMessage = ContactErrorMessages.ContentRequired)]
        [MinLength(10, ErrorMessage = ContactErrorMessages.ContentLength)]
        public string Content { get; set; }
    }
}
