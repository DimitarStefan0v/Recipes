﻿namespace Recipes.Web.ViewModels.Recipes
{
    using AutoMapper;
    using global::Recipes.Common;
    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;

    public class RecipeInListViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryColor { get; set; }

        public string Url => $"/r-{this.Id}/{this.Name.Replace(" ", "-").Replace(",", "-")}";

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipeInListViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(r => r.Image.PictureUrl))
                .ForMember(x => x.CategoryColor, opt => opt.MapFrom(r => r.Category.Color == null ? GlobalConstants.CategoryDefaultColor : r.Category.Color));
        }
    }
}
