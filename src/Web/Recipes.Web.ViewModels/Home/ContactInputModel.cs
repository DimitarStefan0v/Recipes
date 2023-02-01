namespace Recipes.Web.ViewModels.Home
{
    using System.ComponentModel.DataAnnotations;

    using global::Recipes.Common.Constants;

    public class ContactInputModel
    {
        [Display(Name = "Вашите имена")]
        [Required(ErrorMessage = ContactErrorMessages.NameRequired)]
        [MinLength(3, ErrorMessage = ContactErrorMessages.NameLength)]
        public string Name { get; set; }

        [Display(Name = "Вашият имейл адрес")]
        [Required(ErrorMessage = ContactErrorMessages.EmailRequired)]
        [EmailAddress(ErrorMessage = ContactErrorMessages.EmailNotValid)]
        public string Email { get; set; }

        [Display(Name = "Заглавие на съобщението")]
        [Required(ErrorMessage = ContactErrorMessages.TitleRequired)]
        [MinLength(5, ErrorMessage = ContactErrorMessages.TitleLength)]
        public string Title { get; set; }

        [Display(Name = "Съдържание на съобщението")]
        [Required(ErrorMessage = ContactErrorMessages.ContentRequired)]
        [MinLength(10, ErrorMessage = ContactErrorMessages.ContentLength)]
        public string Content { get; set; }

        public string Ip { get; set; }
    }
}
