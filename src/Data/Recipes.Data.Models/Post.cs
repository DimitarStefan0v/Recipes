namespace Recipes.Data.Models
{
    using System.Collections.Generic;

    using Recipes.Data.Common.Models;

    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }

        public string Name { get; set; }

        public string Content { get; set; }

        public int ViewsCount { get; set; }

        public bool IsApproved { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
