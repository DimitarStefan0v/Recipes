using RecipesApp.Core.Models;

namespace RecipesApp.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsersAsync();
    }
}
