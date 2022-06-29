using RecipesApp.Core.Contracts;
using RecipesApp.Core.Models;
using RecipesApp.Infrastructure.Data;
using RecipesApp.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesApp.Core.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IApplicationDbRepository repo;

        public CommentsService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddAsync(CommentInputModel input, string userId)
        {
            var comment = new Comment
            {
                Content = input.Content,
                CreatedOn = DateTime.UtcNow,
                AddedByUserId = userId,
                RecipeId = input.RecipeId,
            };

            await repo.AddAsync(comment);
            await repo.SaveChangesAsync();
        }
    }
}
