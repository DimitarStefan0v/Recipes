﻿namespace Recipes.Web.ViewModels.Posts
{
    using System;
    using System.Globalization;

    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;

    public class PostInListViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ViewsCount { get; set; }

        public int CommentsCount { get; set; }

        public string AddedByUserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string DateAsString => this.CreatedOn.ToString("ddd, MM dd, yyyy HH:mm", new CultureInfo("bg-Bg"));
    }
}
