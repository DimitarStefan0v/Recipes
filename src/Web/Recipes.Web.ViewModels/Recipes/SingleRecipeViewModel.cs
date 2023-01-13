namespace Recipes.Web.ViewModels.Recipes
{
    using System;

    using AutoMapper;
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

        public string ImageUrl { get; set; }

        public string AddedByUserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, SingleRecipeViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Image.PictureUrl));
        }
    }
}
