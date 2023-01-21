namespace Recipes.Web.ViewModels.Recipes
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using global::Recipes.Common.Constants;
    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;

    public class EditRecipeInputModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        [Display(Name = "Заглавие на рецептата")]
        [Required(ErrorMessage = RecipeErrorMessages.NameRequired)]
        [MinLength(3, ErrorMessage = RecipeErrorMessages.NameLength)]
        [MaxLength(50, ErrorMessage = RecipeErrorMessages.NameLength)]
        public string Name { get; set; }

        [Display(Name = "Начин на приготвяне")]
        [Required(ErrorMessage = RecipeErrorMessages.DescriptionRequired)]
        [MinLength(10, ErrorMessage = RecipeErrorMessages.DescriptionLength)]
        public string Description { get; set; }

        [Display(Name = "Време за приготвяне /минути/")]
        public int? PreparationTime { get; set; }

        [Display(Name = "Време за готвене /минути/")]
        public int? CookingTime { get; set; }

        [Display(Name = "Порции")]
        public int? PortionsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, EditRecipeInputModel>()
                .ForMember(x => x.PreparationTime, opt => opt.MapFrom(r => (int)r.PreparationTime.Value.TotalMinutes))
                .ForMember(x => x.CookingTime, opt => opt.MapFrom(r => (int)r.CookingTime.Value.TotalMinutes));
        }
    }
}
