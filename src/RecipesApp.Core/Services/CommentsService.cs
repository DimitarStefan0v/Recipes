using RecipesApp.Core.Contracts;
using RecipesApp.Core.Models;
using RecipesApp.Infrastructure.Data;
using RecipesApp.Infrastructure.Data.Repositories;

namespace RecipesApp.Core.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IApplicationDbRepository repo;
        private readonly ApplicationDbContext applicationDbContext;

        public CommentsService(IApplicationDbRepository _repo, ApplicationDbContext _applicationDbContext)
        {
            repo = _repo;
            applicationDbContext = _applicationDbContext;
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

        public async Task DeleteAsync(int id)
        {
            var comment = repo.All<Comment>().Where(x => x.Id == id).FirstOrDefault();

            if (comment != null)
            {
                applicationDbContext.Remove(comment);
                await repo.SaveChangesAsync();
            }

        }
    }
}
