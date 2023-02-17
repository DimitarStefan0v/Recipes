namespace Recipes.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using AutoMapper;
    using global::Recipes.Common;
    using global::Recipes.Common.Constants;
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

        public int ViewsCount { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        public double AverageVotesValue { get; set; }

        public int VotesCount { get; set; }

        public bool IsApproved { get; set; }

        public bool IsRecipeInFavorites { get; set; }

        public IEnumerable<IngredientViewModel> Ingredients { get; set; }

        public string AddedByUserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string DateAsString => this.CreatedOn.ToString("ddd, MM dd, yyyy HH:mm", new CultureInfo("bg-Bg"));

        public int CommentsCount { get; set; }

        public int CommentsPerPageCount => 5;

        public int CommentsPagesCount => (int)Math.Ceiling((double)this.CommentsCount / this.CommentsPerPageCount);

        [Display(Name = "Добави коментар")]
        [Required(ErrorMessage = CommentErrorMessages.ContentRequired)]
        [MinLength(10, ErrorMessage = CommentErrorMessages.ContentLength)]
        [MaxLength(500, ErrorMessage = CommentErrorMessages.ContentLength)]
        public string CommentContent { get; set; }

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
