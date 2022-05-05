using Microsoft.EntityFrameworkCore;
using RecipesApp.Core.Contracts;
using RecipesApp.Core.Models;
using RecipesApp.Infrastructure.Data.Identity;
using RecipesApp.Infrastructure.Data.Repositories;

namespace RecipesApp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepository repo;

        public UserService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsersAsync()
        {
            var users = await repo.All<ApplicationUser>()
                .Select(u => new UserListViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName
                }).ToListAsync();

            return users;
        }
    }
}
