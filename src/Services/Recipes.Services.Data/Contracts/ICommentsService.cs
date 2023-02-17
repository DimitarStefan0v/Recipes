namespace Recipes.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task AddCommentAsync(int id, string content, string userId, bool postComment);

        Task DeleteAsync(int id);

        IEnumerable<T> GetPostComments<T>(int id, int page, int itemsPerPage);

        IEnumerable<T> GetAllUnapproved<T>(int page, int itemsPerPage);

        Task Approve(int id);
    }
}
