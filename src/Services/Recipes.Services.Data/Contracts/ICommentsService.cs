namespace Recipes.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task AddCommentAsync(int id, string content, string userId, bool postComment);
    }
}
