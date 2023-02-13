namespace Recipes.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using global::Recipes.Common.Constants;
    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;
    using global::Recipes.Web.ViewModels.Comments;

    public class SinglePostViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string AddedByUserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string DateAsString => this.CreatedOn.ToString("ddd, MM dd, yyyy HH:mm", new CultureInfo("bg-Bg"));

        public IEnumerable<CommentInListViewModel> Comments { get; set; }

        [Required(ErrorMessage = CommentErrorMessages.ContentRequired)]
        [MinLength(10, ErrorMessage = CommentErrorMessages.ContentLength)]
        [MaxLength(300, ErrorMessage = CommentErrorMessages.ContentLength)]
        public string CommentContent { get; set; }
    }
}
