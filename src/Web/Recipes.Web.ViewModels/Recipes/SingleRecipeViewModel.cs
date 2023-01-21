namespace Recipes.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using global::Recipes.Common;
    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;

    public class SingleRecipeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PreparationTime { get; set; }

        public int CookingTime { get; set; }

        public int TotalTime => this.PreparationTime + this.CookingTime;

        public int PortionsCount { get; set; }

        public string Color { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<IngredientViewModel> Ingredients { get; set; }

        public string AddedByUserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, SingleRecipeViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(r => r.Image.PictureUrl))
                .ForMember(x => x.PreparationTime, opt => opt.MapFrom(r => (int)r.PreparationTime.Value.TotalMinutes))
                .ForMember(x => x.CookingTime, opt => opt.MapFrom(r => (int)r.CookingTime.Value.TotalMinutes))
                .ForMember(x => x.Color, opt => opt.MapFrom(r => r.Category.Color == null ? GlobalConstants.CategoryDefaultColor : r.Category.Color));
        }
    }
}
