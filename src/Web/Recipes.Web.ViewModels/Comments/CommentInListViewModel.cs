namespace Recipes.Web.ViewModels.Comments
{
    using System;
    using System.Globalization;

    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;

    public class CommentInListViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string AddedByUserUserName { get; set; }

        public bool IsApproved { get; set; }

        public DateTime CreatedOn { get; set; }

        public string DateAsString => this.CreatedOn.ToString("dd MMMM yyyy", new CultureInfo("bg-Bg"));
    }
}
