﻿namespace Recipes.Web.ViewModels.Categories
{
    using System;
    using System.Linq;

    using AutoMapper;
    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;

    public class SingleCategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public string ImageUrl { get; set; }

        public int RecipesCount { get; set; }

        public DateTime RecentRecipeDate { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Category, SingleCategoryViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(c => c.Image.PictureUrl))
                .ForMember(x => x.RecentRecipeDate, opt => opt.MapFrom(c => c.Recipes
                                                                    .OrderByDescending(r => r.CreatedOn)
                                                                    .Select(rd => rd.CreatedOn)
                                                                    .FirstOrDefault()));
        }
    }
}
