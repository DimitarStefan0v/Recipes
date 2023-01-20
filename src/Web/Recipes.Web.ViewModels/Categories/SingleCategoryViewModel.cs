namespace Recipes.Web.ViewModels.Categories
{
    using AutoMapper;
    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;

    public class SingleCategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Category, EditCategoryInputModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(c => c.Image.PictureUrl));
        }
    }
}
