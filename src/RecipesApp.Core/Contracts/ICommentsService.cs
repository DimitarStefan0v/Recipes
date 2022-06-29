using RecipesApp.Core.Models;

namespace RecipesApp.Core.Contracts
{
    public interface ICommentsService
    {
        Task AddAsync(CommentInputModel input, string userId);
    }
}
