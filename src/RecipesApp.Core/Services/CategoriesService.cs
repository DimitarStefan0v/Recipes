using RecipesApp.Core.Contracts;
using RecipesApp.Core.Models;
using RecipesApp.Infrastructure.Data;
using RecipesApp.Infrastructure.Data.Repositories;

namespace RecipesApp.Core.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IApplicationDbRepository repo;

        public CategoriesService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public ICollection<CategoriesViewModel> GetAllCategories()
        {
            var categories = repo.All<Category>()
                .Select(c => new CategoriesViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToList();

            return categories;
        }
    }
}
