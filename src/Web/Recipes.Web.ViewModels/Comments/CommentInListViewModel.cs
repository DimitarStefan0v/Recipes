﻿namespace Recipes.Web.ViewModels.Comments
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

        public string DateAsString => this.CreatedOn.ToString("ddd, MM dd, yyyy HH:mm", new CultureInfo("bg-Bg"));

        public string DateAsForRecipeCommentString => this.CreatedOn.ToString("dddd, dd MMMM yyyy", new CultureInfo("bg-Bg"));
    }
}
