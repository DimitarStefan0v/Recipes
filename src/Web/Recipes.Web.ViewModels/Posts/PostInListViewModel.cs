namespace Recipes.Web.ViewModels.Posts
{
    using System;
    using System.Globalization;
    using System.Linq;

    using AutoMapper;
    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;

    public class PostInListViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ViewsCount { get; set; }

        public int CommentsCount { get; set; }

        public string AddedByUserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string DateAsString => this.CreatedOn.ToString("ddd, MM dd, yyyy HH:mm", new CultureInfo("bg-Bg"));

        public string UserNameOfLastComment { get; set; }

        public DateTime LastCommentDate { get; set; }

        public string LastCommentDateAsString => this.LastCommentDate.ToString("ddd, MM dd, yyyy HH:mm", new CultureInfo("bg-Bg"));

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, PostInListViewModel>()
                .ForMember(x => x.UserNameOfLastComment, opt => opt.MapFrom(p => p.Comments
                                                                .OrderByDescending(c => c.CreatedOn)
                                                                .Select(c => c.AddedByUser.UserName)
                                                                .FirstOrDefault()))
                .ForMember(x => x.LastCommentDate, opt => opt.MapFrom(p => p.Comments
                                                                .OrderByDescending(c => c.CreatedOn)
                                                                .Select(c => c.CreatedOn)
                                                                .FirstOrDefault()));
        }
    }
}
